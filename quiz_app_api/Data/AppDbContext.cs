using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data.Entities;
using quiz_app_api.Data.Seeds;

namespace quiz_app_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<UserEntity> UserEntities { get; set; }
	public DbSet<UserAnswerEntity> UserAnswerEntities { get; set; }
	public DbSet<QuestionEntity> QuestionEntities { get; set; }
	public DbSet<SystemStatusEntity> SystemStatusEntities { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		UserSeeder.Seed(modelBuilder);
		QuestionsSeeder.Seed(modelBuilder);
		SystemStatusSeeder.Seed(modelBuilder);
	}
}