using Microsoft.AspNetCore.Mvc;

namespace quiz_app_api.Controllers;

[ApiController]
public class UsersController : Controller
{

	[HttpPost]
	public async Task<IActionResult> CreateUser(string json)
	{
		return null;
	}

}
