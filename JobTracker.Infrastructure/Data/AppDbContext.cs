using JobTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<ApplicatioStatusHistory> ApplicationStatusHistories => Set<ApplicatioStatusHistory>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Email).HasMaxLength(256).IsRequired();
            e.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
            e.Property(u => u.FullName).HasMaxLength(100).IsRequired();
            e.Property(u => u.Role).HasMaxLength(20).IsRequired();

            // Temporary seed user for Week 1 (replaced by real registration in Week 2)
            e.HasData(new User
            {
                Id = 1,
                Email = "demo@jobtracker.local",
                PasswordHash = "TEMP",
                FullName = "Demo User",
                Role = "User",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        });

        b.Entity<Company>(e =>
        {
            e.Property(c => c.Name).HasMaxLength(150).IsRequired();
            e.Property(c => c.Website).HasMaxLength(300);
            e.Property(c => c.Location).HasMaxLength(100);
            e.HasIndex(c => new { c.UserId, c.Name }).IsUnique();
        });

        b.Entity<JobApplication>(e =>
        {
            e.Property(x => x.JobTitle).HasMaxLength(200).IsRequired();
            e.Property(x => x.JobUrl).HasMaxLength(500);
            e.Property(x => x.Notes).HasMaxLength(2000);
            e.Property(x => x.ExpectedSalary).HasPrecision(18, 2);
            e.HasIndex(x => new { x.UserId, x.Status });
            e.HasOne(x => x.Company)
             .WithMany(c => c.Applications)
             .HasForeignKey(x => x.CompanyId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}