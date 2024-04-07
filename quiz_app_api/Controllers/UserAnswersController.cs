using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.JsonModels;
using quiz_app_api.Data.JsonModels.UserAnswers.Input;
using quiz_app_api.Data.JsonModels.UserAnswers.Output;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/UserAnswers")]
[ApiController]
public class UserAnswersController(AppDbContext _context) : Controller
{
	// POST: api/useranswers/CreateUserAnswer
	[HttpPost]
	[Route("CreateUserAnswer")]
	public async Task<ActionResult> CreateUserAnswer([FromBody] CreateUserAnswerJson data)
	{
		var status = new SuccessJson();

		if(data.ApiKey == null)
		{
			return Json(status);
		}

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
		catch(Exception e)
		{
			await Console.Out.WriteLineAsync(e.Message);
			status.Success = false;
			return Json(status);
		}

		status.Success = true;
		return Json(status);
	}

	// GET: api/useranswers/GetUserAnswers
	[HttpGet]
	[Route("GetUserAnswers")]
	public async Task<ActionResult> GetUserAnswers([FromBody] GetUserAnswersJson data)
	{
		if(data.ApiKey == null || !AdminTools.IsAdmin(data.ApiKey))
		{
			return Json(new GetUserAnswersReturnJson());
		}

		var user = await _context.UserEntities.Where(x => x.Id == data.UserId).FirstOrDefaultAsync();

		var userAnswers = await _context.UserAnswerEntities.Include(x => x.Question).Where(x => x.User == user).ToListAsync();

		return Json(userAnswers);
	}
}
