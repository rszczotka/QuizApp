using Microsoft.AspNetCore.Mvc;
using quiz_app_api.Data;
using quiz_app_api.Data.JsonModels.UserAnswers.Input;

namespace quiz_app_api.Controllers;

[Route("api/UserAnswers")]
[ApiController]
public class UserAnswersController(AppDbContext _context) : Controller
{
	// POST: api/useranswers/CreateUserAnswer
	[HttpPost]
	[Route("CreateUserAnswer")]
	public Task<ActionResult> CreateUserAnswer([FromBody] CreateUserAnswerJson data)
	{
		throw new NotImplementedException();
	}

	// GET: api/useranswers/GetUserAnswers
	[HttpGet]
	[Route("GetUserAnswers")]
	public Task<ActionResult> GetUserAnswers([FromBody] GetUserAnswersJson data)
	{
		throw new NotImplementedException();
	}
}
