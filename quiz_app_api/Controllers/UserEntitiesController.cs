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
		// POST: api/users/CreateUser/{"api_key": "administrator-api-key", "user": {}}
		[HttpPost]
		[Route("CreateUser/{jsonData}")]
		public async Task<ActionResult> CreateUser(string jsonData)
		{
			var data = JsonConvert.DeserializeObject<CreateUserJson>(jsonData);
			var status = new SuccessJson();

			// checks for any null value
			if(data == null || data.ApiKey == null || data.User == null)
			{
				status.Success = false;
				return Json(status);
			}

			if(!IsAdmin(data.ApiKey))
			{
				status.Success = false;
				return Json(status);
			}

			try
			{
				var userEntity = new UserEntity
				{
					// id generated automatically
					AccountType = 0,
					Name = data.User.Name,
					Surname = data.User.Surname,
					// login is name.surname, so if name is Jan and surname is Kowalski, then login is jan.kowalski
					Login = $"{data.User.Name.ToLower()}.{data.User.Surname.ToLower()}",
					Password = data.User.Password,
					// API key is surname + password e.g. kowalski396
					ApiKey = $"{data.User.Surname.ToLower()}{data.User.Password}",
					Status = 0
				};

				await _context.SaveChangesAsync();
				_context.UserEntities.Add(userEntity);
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

			// returns all users
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
		[HttpDelete]
		[Route("RemoveUser/{jsonData}")]
		public async Task<ActionResult> DeleteUserEntity(string jsonData)
		{
			var data = JsonConvert.DeserializeObject<RemoveUserJson>(jsonData);
			var status = new SuccessJson();

			if(data == null || data.ApiKey == null)
			{
				status.Success = false;
				return Json(status);
			}

			var userEntity = await _context.UserEntities.FindAsync(data.UserId);
			if(userEntity == null || !IsAdmin(data.ApiKey))
			{
				status.Success = false;
				return Json(status);
			}

			_context.UserEntities.Remove(userEntity);
			await _context.SaveChangesAsync();

			status.Success = true;

			return Json(status);
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
