using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.JsonModels;
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
	public async Task<ActionResult> CreateQuestion([FromBody] CreateQuestionJson data)
	{
        var status = new SuccessJson();

        // checks for any null value
        if (data == null || data.ApiKey == null || data.Question == null)
        {
            status.Success = false;
            return Json(status);
        }

        if (!AdminTools.IsAdmin(data.ApiKey))
        {
            status.Success = false;
            return Json(status);
        }

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
            await Console.Out.WriteLineAsync(e.Message);
            status.Success = false;
            return Json(status);
        }

        status.Success = true;
        return Json(status);
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
	public async Task<ActionResult> GetNextQuestion([FromBody] GetNextQuestionJson data)
	{
		var status = new SuccessJson()
		{
			Success = false
		};

        var systemStatus = await _context.SystemStatusEntities.FirstAsync();

		if (systemStatus.Status != 2)
		{
			Console.Out.WriteLine(systemStatus.Status);
			return Json(status);
		}

        if (data.ApiKey == null)
		{
			return Json(status);
		}

		var result = _context.UserEntities.Where(n => n.Login.Equals(APIKeyGenerator.GetLoginByAPIKey(data.ApiKey)));

		if (result.Count() == 0)
		{
            Console.Out.WriteLine("C0");
            return Json(status);
		}

		var user = result.First();

		if (user == null)
		{
            return Json(status);
		}

		var questionCount = _context.QuestionEntities.Count();

		if (user.Status < 1)
		{
            return Json(status);
		}

        var question = _context.QuestionEntities.ElementAtOrDefault(user.Status - 1);

		if (question == null)
		{
			return Json(status);
		}

        user.Status++;

        await _context.SaveChangesAsync();

		return Json(new NextQuestionJson()
		{
			Id = question.Id,
			Text = question.Text,
			Options = question.Options,
			AvailableTime = question.AvailableTime
		});
	}

	// PUT: api/questions/UpdateQuestion
	[HttpPut]
	[Route("UpdateQuestion")]
	public Task<ActionResult> UpdateQuestion()
	{
		throw new NotImplementedException();
	}
	
	// DELETE: api/questions/RemoveQuestion/
	[HttpDelete]
	[Route("RemoveQuestion")]
	public async Task<ActionResult> DeleteQuestion([FromBody] RemoveQuestionJson data)
	{
        var status = new SuccessJson();

        if (data == null || data.ApiKey == null)
        {
            status.Success = false;
            return Json(status);
        }

		if(!AdminTools.IsAdmin(data.ApiKey))
		{
			status.Success = false;
			return Json(status);
		}

        var questionEntity = await _context.QuestionEntities.FindAsync(data.QuestionId);

        if (questionEntity == null)
        {
            status.Success = false;
            return Json(status);
        }

        _context.QuestionEntities.Remove(questionEntity);
        await _context.SaveChangesAsync();

        status.Success = true;

        return Json(status);
    }
}
