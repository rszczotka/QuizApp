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
				Name = "admin",
				Surname = "admin",
				Login = "admin.admin",
				Password = "0",
				ApiKey = "system",
				Status = 0
			},
			// normal users
			new()
			{
				Id = 2,
				AccountType = 0,
				Name = "Kamil",
				Surname = "Zdun",
				Login = "kamil.zdun",
				Password = "111",
				ApiKey = "zdun111",
				Status = 0
			},
			new()
			{
				Id = 3,
				AccountType = 0,
				Name = "Michał",
				Surname = "Zdunowski",
				Login = "michał.zdun",
				Password = "222",
				ApiKey = "zdunowski222",
				Status = 0
			},
			new()
			{
				Id = 4,
				AccountType = 0,
				Name = "Wojtek",
				Surname = "Zduński",
				Login = "wojtek.zduński",
				Password = "333",
				ApiKey = "zdunski333",
				Status = 0
			}
		});
	}
}
