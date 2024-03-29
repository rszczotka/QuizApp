using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.JsonModels;
using quiz_app_api.Data.JsonModels.Users.Input;
using quiz_app_api.Data.JsonModels.Users.Output;
using quiz_app_api.Misc;

namespace quiz_app_api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(AppDbContext _context) : Controller
{
	// POST: api/users/CreateUser
	[HttpPost]
	[Route("CreateUser")]
	public async Task<ActionResult> CreateUser([FromBody] CreateUserJson data)
	{
		var status = new SuccessJson();

		// checks for any null value
		if(data == null || data.ApiKey == null || data.User == null)
		{
			status.Success = false;
			return Json(status);
		}

		if(!AdminTools.IsAdmin(data.ApiKey))
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
				Status = 0
			};

            _context.UserEntities.Add(userEntity);
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

	// GET: api/users/GetAllUsers/
	[HttpGet]
	[Route("GetAllUsers")]
	public async Task<ActionResult> GetAllUsers([FromBody] GetAllUsersJson data)
	{
		if(data.ApiKey == null || AdminTools.IsAdmin(data.ApiKey))
		{
			return Json(new List<GetAllUsersReturnJson>());
		}

		var allUsers = await _context.UserEntities
			.Select(x => new GetAllUsersReturnJson
			{
				Id = x.Id,
				Name = x.Name,
				Surname = x.Surname,
				Login = x.Login,
				Status = x.Status
			})
			.ToListAsync();

		return Json(allUsers);
	}

	// GET: api/users/GetUsersInQueue/
	[HttpGet]
	[Route("GetUsersInQueue")]
	public async Task<ActionResult> GetUsersInQueue([FromBody] GetUsersInQueueJson data)
	{
		if(data.ApiKey == null || !APIKeyGenerator.ContainsAPIKey(data.ApiKey))
		{
			return Json(new List<GetAllUsersReturnJson>());
		}

		// user status = 1 means it is logged in and in queue
		var usersInQueue = await _context.UserEntities
			.Where(x => x.Status == 1)
			.Select(x => new GetUsersInQueueReturnJson
			{
				Id = x.Id,
				Name = x.Name,
				Surname = x.Surname
			})
			.ToListAsync();

		return Json(usersInQueue);
	}

	// POST: api/users/Login
	[HttpPost]
	[Route("Login")]
	public async Task<ActionResult> Login([FromBody] LoginJson data)
	{

        var user = await _context.UserEntities.Where(x => x.Login == data.Login && x.Password == data.Password).FirstOrDefaultAsync();

        if (user == null)
		{
			return Json(new SuccessJson()
			{
				Success = false
			});
		}

        if (user.AccountType == 0)
        {
            var systemStatus = await _context.SystemStatusEntities.FirstAsync();

            if (systemStatus.Status == 0)
            {
                return Json(new SuccessJson()
                {
                    Success = false
                });
            }
        }

        if (user.Status == 0)
		{
			user.Status = 1;
			await _context.SaveChangesAsync();
		}

		var response = new LoginReturnJson
		{
			Id = user.Id,
			AccountType = user.AccountType,
			Name = user.Name,
			Surname = user.Surname,
			Login = user.Login,
			ApiKey = APIKeyGenerator.GetOrGenerateAPIKey(user.AccountType, user.Login, user.Password),
			Status = user.Status
		};

		return Json(response);
	}

	// PUT: api/users/UpdateUser
	[HttpPut]
	[Route("UpdateUser")]
	public async Task<ActionResult> UpdateUser([FromBody] UpdateUserJson data)
	{
		var status = new SuccessJson();
		var userEntity = await _context.UserEntities.Where(x => x.Id == data.Id).FirstOrDefaultAsync();

		if(userEntity == null || !AdminTools.IsAdmin(data.ApiKey))
		{
			status.Success = false;
			return Json(status);
		}

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
		catch(DbUpdateConcurrencyException)
		{
			status.Success = false;
			return Json(status);
		}

		status.Success = true;
		return Json(status);
	}
	
	// DELETE: api/users/RemoveUser/
	[HttpDelete]
	[Route("RemoveUser")]
	public async Task<ActionResult> DeleteUser([FromBody] RemoveUserJson data)
	{
		var status = new SuccessJson();

		if(data == null || data.ApiKey == null)
		{
			status.Success = false;
			return Json(status);
		}

		if(!AdminTools.IsAdmin(data.ApiKey))
		{
			status.Success = false;
			return Json(status);
		}

		var userEntity = await _context.UserEntities.FindAsync(data.UserId);

		if(userEntity == null)
		{
			status.Success = false;
			return Json(status);
		}

		_context.UserEntities.Remove(userEntity);
		await _context.SaveChangesAsync();

		status.Success = true;

		return Json(status);
	}
}
