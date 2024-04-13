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
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

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
			return StatusCode(500, "Something went wrong while creating new question" + e.Message);
        }

		return StatusCode(201);
    }

	// GET: api/questions/GetAllQuestions/
	[HttpGet("GetAllQuestions/{adminApiKey}")]
	public async Task<IActionResult> GetAllQuestions(string adminApiKey)
	{
		if(!AdminTools.IsAdmin(adminApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

		var allQuestions = await _context.QuestionEntities
			.Select(x => new GetAllQuestionsJson
			{
				Id = x.Id,
				Text = x.Text,
				Options = x.Options,
				CorrectAnswer = x.CorrectAnswer,
				AvailabelTime = x.AvailableTime
			})
			.ToListAsync();

		return StatusCode(200, allQuestions);
	}

	// GET: api/questions/GetNextQuestion/
	[HttpGet("GetNextQuestion/{apiKey}")]
	public async Task<IActionResult> GetNextQuestion(string apiKey)
	{
		if(!AdminTools.IsUser(apiKey))
			return StatusCode(400, "Not a user API key, or user not logged in");
		if((await _context.SystemStatusEntities.FirstAsync()).Status != 2)
			return StatusCode(403, "System status is not 2 (quiz)");

		var user = await _context.UserEntities.Where(n => n.Login == APIKeyGenerator.GetLoginByAPIKey(apiKey)).FirstAsync();

		if(user.Status > await _context.QuestionEntities.CountAsync())
		{
			user.EndTime = DateTime.Now;
			await _context.SaveChangesAsync();
			return StatusCode(405, "User has answered all questions");
		}

		var nextQuestion = await _context.QuestionEntities.ElementAtAsync(user.Status - 1);
		var systemStatus = await _context.SystemStatusEntities.FirstAsync();

		return StatusCode(200, new NextQuestionJson
		{
			Id = nextQuestion.Id,
			Text = nextQuestion.Text,
			Options = nextQuestion.Options,
			AvailableTime = nextQuestion.AvailableTime,
			TimeFromBeginning = (int)(DateTime.Now - systemStatus.StartTime).TotalSeconds
		});
	}

	[HttpGet("GetQuestionCount")]
	public async Task<IActionResult> GetQuestionCount()
	{
		var questionCount = await _context.QuestionEntities.CountAsync();
		return StatusCode(200, questionCount);
	}

	// PUT: api/questions/UpdateQuestion
	[HttpPut("UpdateQuestion")]
	public IActionResult UpdateQuestion()
	{
		return StatusCode(501);
	}
	
	// DELETE: api/questions/RemoveQuestion/
	[HttpDelete("RemoveQuestion/{adminApiKey}/{id}")]
	public async Task<IActionResult> RemoveQuestion(string adminApiKey, int id)
	{
		if(!AdminTools.IsAdmin(adminApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

		var questionEntity = await _context.QuestionEntities.FindAsync(id);
		if(questionEntity == null)
			return StatusCode(404, "No question with id: " + id);

		_context.QuestionEntities.Remove(questionEntity);
		await _context.SaveChangesAsync();

		return StatusCode(204);
	}
}
