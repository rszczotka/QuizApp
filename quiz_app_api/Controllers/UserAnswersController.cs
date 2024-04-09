using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.JsonModels.Questions;
using quiz_app_api.Data.JsonModels.UserAnswers;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/UserAnswers")]
[ApiController]
public class UserAnswersController(AppDbContext _context) : Controller
{
	// POST: api/useranswers/CreateUserAnswer
	[HttpPost("CreateUserAnswer")]
	public async Task<IActionResult> CreateUserAnswer([FromBody] CreateUserAnswerJson data)
	{
		if(!AdminTools.IsUser(data.ApiKey))
			return StatusCode(400, "Not a user API key, or user not logged in");
		if((await _context.SystemStatusEntities.FirstAsync()).Status != 2)
			return StatusCode(403, "System status is not 2");

		try
		{
			var userAnswerEntity = new UserAnswerEntity
			{
				User = _context.UserEntities.Where(n => n.Login.Equals(APIKeyGenerator.GetLoginByAPIKey(data.ApiKey))).First(),
				Question = _context.QuestionEntities.Where(x => x.Id == data.QuestionId).First(),
				ChosenOption = data.ChosenOption,
				EndTime = DateTime.Now
			};

			_context.UserAnswerEntities.Add(userAnswerEntity);
			await _context.SaveChangesAsync();
		}
		catch(Exception ex)
		{
			return StatusCode(500, "Something went wrong while creating new user answer");
		}

		return StatusCode(201);
	}

	// GET: api/useranswers/GetUserAnswers
	[HttpGet("GetUserAnswers/{adminApiKey}/{userId}")]
	public async Task<IActionResult> GetUserAnswers(string adminApiKey, int userId)
	{
		if(!AdminTools.IsAdmin(adminApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

		var user = await _context.UserEntities.Where(x => x.Id == userId).FirstOrDefaultAsync();

		var userAnswers = await _context.UserAnswerEntities.Include(x => x.Question).Where(x => x.User == user).ToListAsync();

		return StatusCode(200, userAnswers.Select(x => new GetUserAnswersReturnJson
		{
			Question = new GetAllQuestionsReturnJson
			{
				Id = x.Question.Id,
				Text = x.Question.Text,
				Options = x.Question.Options,
				CorrectAnswer = x.Question.CorrectAnswer,
				AvailabelTime = x.Question.AvailableTime
			},
			ChosenOption = x.ChosenOption
		}));
	}
}
