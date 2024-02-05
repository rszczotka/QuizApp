using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using quiz_app_api.Data.Entities;

namespace quiz_app_api.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> UserEntities { get; set; }
    public DbSet<UserAnswerEntity> UserAnswerEntities { get; set; }
    public DbSet<QuestionEntity> QuestionEntities { get; set; }
    public DbSet<SystemStatusEntity> SystemStatusEntities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seeds
        base.OnModelCreating(modelBuilder);
    }

}