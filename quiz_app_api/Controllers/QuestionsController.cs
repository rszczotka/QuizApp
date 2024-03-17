using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.JsonModels.Questions.Input;
using quiz_app_api.Data.JsonModels.Questions.Output;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/questions")]
[ApiController]
public class QuestionsController(AppDbContext _context) : Controller
{
	// POST: api/questions/CreateQuestion
	[HttpPost]
	[Route("CreateQuestion")]
	public async Task<ActionResult> CreateQuestion()
	{
		throw new NotImplementedException();
	}

	// GET: api/questions/GetAllQuestions/
	[HttpGet]
	[Route("GetAllQuestions")]
	public async Task<ActionResult> GetAllQuestions([FromBody] GetAllQuestionsJson data)
	{
		if(data.ApiKey == null || !AdminTools.IsAdmin(data.ApiKey))
		{
			return Json(new List<GetAllQuestionsReturnJson>());
		}

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

		return Json(allQuestions);
	}

	// GET: api/questions/GetNextQuestion/
	[HttpGet]
	[Route("GetNextQuestion")]
	public async Task<ActionResult> GetNextQuestion()
	{
		throw new NotImplementedException();
	}

	// PUT: api/questions/UpdateQuestion
	[HttpPut]
	[Route("UpdateQuestion")]
	public async Task<ActionResult> UpdateQuestion()
	{
		throw new NotImplementedException();
	}
	
	// DELETE: api/questions/RemoveQuestion/
	[HttpDelete]
	[Route("RemoveQuestion")]
	public async Task<ActionResult> DeleteQuestion()
	{
		throw new NotImplementedException();
	}
}
