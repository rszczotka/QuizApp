using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.JsonModels;
using quiz_app_api.Data.JsonModels.SystemStatus;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/systemstatus")]
[ApiController]
public class SystemStatusController(AppDbContext _context) : Controller
{
	// GET: api/systemstatus/GetSystemStatus
	[HttpGet]
	[Route("GetSystemStatus")]
	public async Task<ActionResult> GetSystemStatus()
	{
		var systemStatus = await _context.SystemStatusEntities.FirstAsync();
		
		return Json(new GetSystemStatusReturnJson { Status = systemStatus.Status });
	}

	// PUT: api/systemstatus/UpdateSystemStatus
	[HttpPut]
	[Route("UpdateSystemStatus")]
	public async Task<ActionResult> UpdateSystemStatus([FromBody] UpdateSystemStatus data)
	{
		var status = new SuccessJson();
		var systemStatusEntity = await _context.SystemStatusEntities.FirstAsync();

		if(!AdminTools.IsAdmin(data.ApiKey))
		{
			status.Success = false;
			return Json(status);
		}

		// if passed status < 0, then status = 0; if passed status > 3, then status = 3
		systemStatusEntity.Status = Math.Min(Math.Max(data.Status, 0), 3);

		if(systemStatusEntity.Status == 0)
		{
			APIKeyGenerator.FlushApiKeys();
		}

		_context.Entry(systemStatusEntity).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch(DbUpdateConcurrencyException)
		{
			status.Success = false;
			return Json(status);
		}

		status.Success = true;
		return Json(status);
	}
}
