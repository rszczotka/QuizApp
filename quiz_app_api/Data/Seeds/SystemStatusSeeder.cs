using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;

namespace quiz_app_api.Data.Seeds;

public class SystemStatusSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SystemStatusEntity>().HasData(
			new SystemStatusEntity
			{
				Id = 1,
				Status = 0,
				AvailableTime = 45,
				UpdatedAt = DateTime.UtcNow
			}
		);
	}
}
