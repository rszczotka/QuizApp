using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.JsonModels.Questions;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/questions")]
[ApiController]
public class QuestionsController(AppDbContext _context) : Controller
{
	// POST: api/questions/CreateQuestion
	[HttpPost("CreateQuestion")]
	public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionJson data)
	{
        if(!AdminTools.IsAdmin(data.ApiKey))
			return Unauthorized("Not an admin API key, or admin is not logged in");

        try
        {
            var questionEntity = new QuestionEntity
            {
                Text = data.Question.Text,
				Options = data.Question.Options,
				CorrectAnswer = data.Question.CorrectAnswer,
				AvailableTime = data.Question.AvailableTime
            };

            _context.QuestionEntities.Add(questionEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
			return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong while creating new question" + e.Message);
        }

		return Created();
    }

	// GET: api/questions/GetAllQuestions/
	[HttpGet("GetAllQuestions/{adminApiKey}")]
	public async Task<IActionResult> GetAllQuestions(string adminApiKey)
	{
		if(!AdminTools.IsAdmin(adminApiKey))
			return Unauthorized("Not an admin API key, or admin is not logged in");

		var allQuestions = await _context.QuestionEntities
			.Select(x => new GetAllQuestionsReturnJson
			{
				Id = x.Id,
				Text = x.Text,
				Options = x.Options,
				CorrectAnswer = x.CorrectAnswer,
				AvailabelTime = x.AvailableTime
			})
			.ToListAsync();

		return Ok(allQuestions);
	}

	// GET: api/questions/GetNextQuestion/
	[HttpGet("GetNextQuestion/{apiKey}")]
	public async Task<IActionResult> GetNextQuestion(string apiKey)
	{
		if(!AdminTools.IsUser(apiKey))
			return BadRequest("Not a user API key, or user not logged in");
		if((await _context.SystemStatusEntities.FirstAsync()).Status != 2)
			return Forbid("System status is not 2");

		var user = await _context.UserEntities.Where(n => n.Login == APIKeyGenerator.GetLoginByAPIKey(apiKey)).FirstAsync();

		if(user.Status > 1 + await _context.QuestionEntities.CountAsync())
			return StatusCode(StatusCodes.Status405MethodNotAllowed, "User has answered all questions");

		var nextQuestion = await _context.QuestionEntities.ElementAtAsync(user.Status - 1);

		user.Status++;
		await _context.SaveChangesAsync();

		return Ok(new NextQuestionJson
		{
			Id = nextQuestion.Id,
			Text = nextQuestion.Text,
			Options = nextQuestion.Options,
			AvailableTime = nextQuestion.AvailableTime
		});
	}

	// PUT: api/questions/UpdateQuestion
	[HttpPut("UpdateQuestion")]
	public IActionResult UpdateQuestion()
	{
		return StatusCode(StatusCodes.Status501NotImplemented);
	}
	
	// DELETE: api/questions/RemoveQuestion/
	[HttpDelete("RemoveQuestion/{adminApiKey}/{id}")]
	public async Task<IActionResult> DeleteQuestion(string adminApiKey, int id)
	{
		if(!AdminTools.IsAdmin(adminApiKey))
			return Unauthorized("Not an admin API key, or admin is not logged in");

		var questionEntity = await _context.QuestionEntities.FindAsync(id);
		if(questionEntity == null)
			return NotFound("No question with id: " + id);

		_context.QuestionEntities.Remove(questionEntity);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
