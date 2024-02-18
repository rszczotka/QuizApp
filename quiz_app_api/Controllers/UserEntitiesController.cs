using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.JsonModels;
using quiz_app_api.Data.JsonModels.Users;

namespace quiz_app_api.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserEntitiesController(AppDbContext _context) : Controller
	{
		// GET: api/users/CreateUser/{"api_key": "administrator-api-key", "user": {}}
		/*[HttpGet]
		[Route("CreateUser/{jsonData}")]*/

		// GET: api/users/GetAllUsers/{"api_key": "administrator-api-key"}
		[HttpGet]
		[Route("GetAllUsers/{jsonData}")]
		public async Task<ActionResult> GetAllUsers(string jsonData)
		{
			var data = JsonConvert.DeserializeObject<GetAllUsersJson>(jsonData);

			if(data == null || data.ApiKey == null)
			{
				return Json(new List<UserEntity>());
			}

			if(!IsAdmin(data.ApiKey))
			{
				return Json(new List<UserEntity>());
			}

			return Json(await _context.UserEntities.ToListAsync());
		}

		// PUT: api/UserEntities/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUserEntity(int id, UserEntity userEntity)
		{
			if(id != userEntity.Id)
			{
				return BadRequest();
			}

			_context.Entry(userEntity).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch(DbUpdateConcurrencyException)
			{
				if(!UserEntityExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/UserEntities
		[HttpPost]
		public async Task<ActionResult<UserEntity>> PostUserEntity(UserEntity userEntity)
		{
			_context.UserEntities.Add(userEntity);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetUserEntity", new { id = userEntity.Id }, userEntity);
		}

		// DELETE: api/users/RemoveUser/{"user_id": 0, "api_key": "administrator-api-key"}
		[HttpGet]
		[Route("RemoveUser/{jsonData}")]
		public async Task<ActionResult<SuccessJson>> DeleteUserEntity(string jsonData)
		{
			var data = JsonConvert.DeserializeObject<RemoveUserJson>(jsonData);
			var status = new SuccessJson();

			if(data == null || data.ApiKey == null)
			{
				status.Success = false;
				return status;
			}

			var userEntity = await _context.UserEntities.FindAsync(data.UserId);
			if(userEntity == null || !IsAdmin(data.ApiKey))
			{
				status.Success = false;
				return status;
			}

			_context.UserEntities.Remove(userEntity);
			await _context.SaveChangesAsync();

			status.Success = true;

			return status;
		}

		private bool IsAdmin(string apiKey)
		{
			// looks for users with admin account type with passed API key, if there are none return false, otherwise true
			return _context.UserEntities
				.Where(x => x.AccountType == 1)
				.Select(x => x.ApiKey == apiKey)
				.FirstOrDefault();
		}

		private bool UserEntityExists(int id)
		{
			return _context.UserEntities.Any(e => e.Id == id);
		}
	}
}
