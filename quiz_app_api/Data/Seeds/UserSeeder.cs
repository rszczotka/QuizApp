using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;

namespace quiz_app_api.Data.Seeds;

public class UserSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>
		{
			// admin
			new()
			{
				Id = 1,
				AccountType = 1,
				Name = "Admin",
				Surname = "Admin",
				Login = "admin",
				Password = 111,
				ApiKey = "admin111",
				Status = 0
			},
			// normal users
			new()
			{
				Id = 2,
				AccountType = 0,
				Name = "user1",
				Surname = "a",
				Login = "user1a",
				Password = 222,
				ApiKey = "user1222",
				Status = 0
			},


			new()
			{
				Id = 3,
				AccountType = 0,
				Name = "user2",
				Surname = "a",
				Login = "user2a",
				Password = 333,
				ApiKey = "user2222",
				Status = 0
			},
			new()
			{
				Id = 4,
				AccountType = 0,
				Name = "user3",
				Surname = "a",
				Login = "user3a",
				Password = 333,
				ApiKey = "user3222",
				Status = 0
			}
		});
	}
}
