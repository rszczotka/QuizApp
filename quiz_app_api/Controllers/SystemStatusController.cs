using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.JsonModels.SystemStatus;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/systemstatus")]
[ApiController]
public class SystemStatusController(AppDbContext _context) : Controller
{
	// GET: api/systemstatus/GetSystemStatus
	[HttpGet("GetSystemStatus")]
	public async Task<IActionResult> GetSystemStatus()
	{
		var systemStatus = await _context.SystemStatusEntities.FirstAsync();

		if((DateTime.Now - (systemStatus.StartTime != DateTime.MinValue ? systemStatus.StartTime : DateTime.Now)).TotalSeconds > (await _context.SystemStatusEntities.FirstAsync()).AvailableTime * 60)
		{
			systemStatus.Status = 3;

			var usersInQuiz = await _context.UserEntities.Where(x => x.Status > 1 && x.EndTime == DateTime.MinValue).ToListAsync();
			var endTime = DateTime.Now;

			foreach(var userInQuiz in usersInQuiz)
			{
				userInQuiz.EndTime = endTime;
			}

			await _context.SaveChangesAsync();
		}
		
		return StatusCode(200, systemStatus.Status);
	}

	// PUT: api/systemstatus/UpdateSystemStatus
	[HttpPut("UpdateSystemStatus")]
	public async Task<IActionResult> UpdateSystemStatus([FromBody] UpdateSystemStatus data)
	{
		if(!AdminTools.IsAdmin(data.ApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");
		if(data.Status > 3 || data.Status < 0)
			return StatusCode(400, "Target system status out of range");

		var systemStatusEntity = await _context.SystemStatusEntities.FirstAsync();

		systemStatusEntity.Status = data.Status;

		switch(systemStatusEntity.Status)
		{
			case 0:
				APIKeyGenerator.FlushApiKeys();
				systemStatusEntity.StartTime = DateTime.MinValue;
				var loggedInUsers = await _context.UserEntities.Where(x => x.Status != 0).ToListAsync();

				foreach(var user in loggedInUsers)
					user.Status = 0;

				await _context.SaveChangesAsync();
				break;
			case 2:
				loggedInUsers = await _context.UserEntities.Where(x => x.Status != 0).ToListAsync();
				var startTime = DateTime.Now;

				systemStatusEntity.StartTime = startTime;

				foreach(var user in loggedInUsers)
					user.StartTime = startTime;

				break;
			case 3:
				// users that didn't answer all questions
				var usersInQuiz = await _context.UserEntities.Where(x => x.Status > 0 && x.EndTime == DateTime.MinValue).ToListAsync();
				var endTime = DateTime.Now;

				foreach(var user in usersInQuiz)
					user.EndTime = endTime;

				await _context.SaveChangesAsync();
				break;
		}

		systemStatusEntity.UpdatedAt = DateTime.Now;
		_context.Entry(systemStatusEntity).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch(DbUpdateConcurrencyException ex)
		{
			return StatusCode(500, "Something went wrong while updating system status: " + ex.Message);
		}

		return StatusCode(204);
	}
}
