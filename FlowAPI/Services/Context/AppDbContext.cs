using FlowApi.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using FlowDb.Services.Token;

namespace FlowAPI.Services.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<UserModel> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var adminUser = new UserModel
        {
            Name = "admin",
            Id = Guid.NewGuid(),
            Email = "admin@test.vzl",
            Password = new Hasher().HashPassword("admin1234"),
            SuperAdmin = true
        };

        modelBuilder.Entity<UserModel>().HasData(adminUser);
    }
}