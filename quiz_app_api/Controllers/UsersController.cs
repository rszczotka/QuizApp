using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.JsonModels.Users;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(AppDbContext _context) : Controller
{
	// POST: api/users/CreateUser
	[HttpPost("CreateUser")]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserJson data)
	{
		if(!AdminTools.IsAdmin(data.ApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

		try
		{
			var userEntity = new UserEntity
			{
				AccountType = 0,
				Name = data.User.Name,
				Surname = data.User.Surname,
				// login is name.surname, so if name is Jan and surname is Kowalski, then login is jan.kowalski
				Login = $"{data.User.Name.ToLower()}.{data.User.Surname.ToLower()}",
				Password = data.User.Password,
				Status = 0
			};

			_context.UserEntities.Add(userEntity);
			await _context.SaveChangesAsync();
		}
		catch(Exception ex)
		{
			return StatusCode(500, "Something went wrong while creating new user: " + ex.Message);
		}

		return StatusCode(201);
	}

	// GET: api/users/GetAllUsers/
	[HttpGet("GetAllUsers/{adminApiKey}")]
	public async Task<IActionResult> GetAllUsers(string adminApiKey)
	{
		if(!AdminTools.IsAdmin(adminApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

		var allUsers = await _context.UserEntities
			.Select(x => new GetAllUsersJson
			{
				Id = x.Id,
				Name = x.Name,
				Surname = x.Surname,
				Login = x.Login,
				Password = x.Password,
				Status = x.Status,
				StartTime = x.StartTime,
				EndTime = x.EndTime
			})
			.ToListAsync();

		return StatusCode(200, allUsers);
	}

	// GET: api/users/GetUsersInQueue/
	[HttpGet("GetUsersInQueue/{apiKey}")]
	public async Task<IActionResult> GetUsersInQueue(string apiKey)
	{
		if(!AdminTools.IsUser(apiKey))
			return StatusCode(400, "Not a user API key, or user not logged in");
		
		// user status = 1 means it is logged in end therefore in queue
		var usersInQueue = await _context.UserEntities
			.Where(x => x.Status == 1 && x.AccountType == 0)
			.Select(x => new GetUsersInQueueJson
			{
				Id = x.Id,
				Name = x.Name,
				Surname = x.Surname
			})
			.ToListAsync();

		return StatusCode(200, usersInQueue);
	}

	// POST: api/users/Login
	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] LoginJson data)
	{
		var user = await _context.UserEntities.Where(x => x.Login == data.Login && x.Password == data.Password).FirstOrDefaultAsync();

		if(user == null)
			return StatusCode(400, "No user found with login = " +  data.Login + " and password = " + data.Password);
		if(user.AccountType != 1 && (await _context.SystemStatusEntities.FirstAsync()).Status == 0)
			return StatusCode(403, "System status is 0 (shut down)");

		if(user.Status == 0)
		{
			user.Status = 1;
			await _context.SaveChangesAsync();
		}

		return StatusCode(200, new LoginReturnJson
		{
			Id = user.Id,
			AccountType = user.AccountType,
			Name = user.Name,
			Surname = user.Surname,
			Login = user.Login,
			ApiKey = APIKeyGenerator.GetOrGenerateAPIKey(user.AccountType, user.Login, user.Password),
			Status = user.Status
		});
	}

	// PUT: api/users/UpdateUser
	[HttpPut("UpdateUser")]
	public async Task<IActionResult> UpdateUser([FromBody] UpdateUserJson data)
	{
		if(!AdminTools.IsAdmin(data.ApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

		var userEntity = await _context.UserEntities.Where(x => x.Id == data.Id).FirstOrDefaultAsync();

		if(userEntity == null)
			return StatusCode(400, "No user with id: " + data.Id);

		userEntity.Name = data.User.Name;
		userEntity.Surname = data.User.Surname;
		userEntity.Password = data.User.Password;
		userEntity.Status = data.User.Status;
		userEntity.Login = $"{data.User.Name.ToLower()}.{data.User.Surname.ToLower()}";

		_context.Entry(userEntity).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch(DbUpdateConcurrencyException ex)
		{
			return StatusCode(500, "Something went wrong while updating user: " + ex.Message);
		}

		return StatusCode(204);
	}
	
	// DELETE: api/users/RemoveUser/
	[HttpDelete("RemoveUser/{adminApiKey}/{userId}")]
	public async Task<IActionResult> RemoveUser(string adminApiKey, int userId)
	{
		if(!AdminTools.IsAdmin(adminApiKey))
			return StatusCode(401, "Not an admin API key, or admin is not logged in");

		var userEntity = await _context.UserEntities.FindAsync(userId);

		if(userEntity == null)
			return StatusCode(404, "No user with id: " + userId);

		_context.UserEntities.Remove(userEntity);
		await _context.SaveChangesAsync();

		return StatusCode(204);
	}
}
