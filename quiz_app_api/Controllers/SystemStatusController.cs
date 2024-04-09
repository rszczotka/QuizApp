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
		
		return StatusCode(200, systemStatus.Status);
	}

	// PUT: api/systemstatus/UpdateSystemStatus
	[HttpPut("UpdateSystemStatus")]
	public async Task<IActionResult> UpdateSystemStatus([FromBody] UpdateSystemStatus data)
	{
		if(!AdminTools.IsAdmin(data.ApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");
		
		var systemStatusEntity = await _context.SystemStatusEntities.FirstAsync();

		if(systemStatusEntity.Status > 3 || systemStatusEntity.Status < 0)
			return StatusCode(400, "Target system status out of range");

		systemStatusEntity.Status = data.Status;

		switch(systemStatusEntity.Status)
		{
			case 0:
				APIKeyGenerator.FlushApiKeys();
				break;
			case 2:
				var usersInQueue = _context.UserEntities.Where(x => x.Status == 1).ToList();

				foreach(var user in usersInQueue)
				{
					user.StartTime = DateTime.Now;
				}

				await _context.SaveChangesAsync();
				break;
		}

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
