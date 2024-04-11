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
			var user = await _context.UserEntities.Where(n => n.Login.Equals(APIKeyGenerator.GetLoginByAPIKey(data.ApiKey))).FirstAsync();
			user.Status++;

			var userAnswerEntity = new UserAnswerEntity
			{
				User = user,
				Question = _context.QuestionEntities.Where(x => x.Id == data.QuestionId).First(),
				ChosenOption = data.ChosenOption,
				EndTime = DateTime.Now
			};

			_context.UserAnswerEntities.Add(userAnswerEntity);
			await _context.SaveChangesAsync();
		}
		catch(Exception ex)
		{
			return StatusCode(500, "Something went wrong while creating new user answer: " + ex.Message);
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

		return StatusCode(200, userAnswers.Select(x => new GetUserAnswersJson
		{
			Question = new GetAllQuestionsJson
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

	// GET: api/useranswers/GetLeaderboard
	[HttpGet("GetLeaderboard/{apiKey}")]
	public async Task<IActionResult> GetLeaderboard(string apiKey)
	{
		if(!AdminTools.IsUser(apiKey))
			return StatusCode(400, "Not a user API key, or user not logged in");
		if((await _context.SystemStatusEntities.FirstAsync()).Status != 3)
			return StatusCode(403, "System status is not 3 (results)");

		var users = await _context.UserEntities.Where(x => x.EndTime != DateTime.MinValue && x.AccountType == 0).ToListAsync();
		var userResults = users.Select(x => new GetLeaderboardJson
		{
			User = new GetLeaderboardJson.UserJson
			{
				Id = x.Id,
				Name = x.Name,
				Surname = x.Surname,
				StartTime = x.StartTime,
				EndTime = x.EndTime
			},
			CorrectAnswers = _context.UserAnswerEntities.Where(x => x.User.Id == x.Id && x.ChosenOption == x.Question.CorrectAnswer).Count(),
			WrongAnswers = _context.UserAnswerEntities.Where(x => x.User.Id == x.Id && x.ChosenOption != x.Question.CorrectAnswer).Count()
		}).ToList();

		return StatusCode(200, userResults
			.OrderByDescending(x => x.CorrectAnswers)
			.ThenBy(x => x.User.EndTime)
			.ToList());
	}
}
