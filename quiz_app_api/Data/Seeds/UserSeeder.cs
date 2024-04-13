using CsvHelper;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.Seeds.Users;
using System.Globalization;

namespace quiz_app_api.Data.Seeds;

public class UserSeeder
{
	private static readonly string UsersFile = "Data/Seeds/Users/users.csv";

	public static void Seed(ModelBuilder modelBuilder)
	{
		var usersCsv = new List<UserCsv>();

		using(var reader = new StreamReader(UsersFile))
		using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			usersCsv = csv.GetRecords<UserCsv>().ToList();
		}

		var users = usersCsv.Select((x, position) => new UserEntity
		{
			Id = position + 2,
			AccountType = 0,
			Name = x.FirstName,
			Surname = x.LastName,
			Class = x.Class.ToString(),
			Login = $"{x.FirstName.ToLower()}.{x.LastName.ToLower()}",
			Password = GeneratePassword(),
			Status = 0
		}).ToList();

		users.Insert(0, new UserEntity
		{
			Id = 1,
			AccountType = 1,
			Name = "admin",
			Surname = "admin",
			Class = "3TP",
			Login = "admin.admin",
			Password = "0",
			Status = 0
		});

		modelBuilder.Entity<UserEntity>().HasData(users);
	}

	private static string GeneratePassword()
	{
		var random = new Random();
		var password = "";

		for(int i = 0; i < 3; i++)
		{
			password += random.Next(1, 10).ToString();
		}

		return password;
	}
}
