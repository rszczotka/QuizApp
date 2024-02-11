using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;

namespace quiz_app_api.Data.Seeds;

public class UserSeeder
{
	public static async Task Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>
		{
			// admin
			new UserEntity
			{
				Id = 1,
				AccountType = 0,
				Name = "Admin",
				Surname = "Admin",
				Login = "admin",
				Password = 111,
				ApiKey = "admin111",
				Status = 0
			},
			// normal user
			new UserEntity
			{
				Id = 2,
				AccountType = 1,
				Name = "Imię",
				Surname = "Nazwisko",
				Login = "imięnazwisko",
				Password = 222,
				ApiKey = "Imię222",
				Status = 0
			},
		});
	}
}
