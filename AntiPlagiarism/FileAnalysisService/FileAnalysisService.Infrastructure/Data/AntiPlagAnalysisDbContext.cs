using FileAnalysisService.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FileAnalysisService.Infrastructure.Data;

public class AntiPlagAnalysisDbContext : DbContext
{
    public AntiPlagAnalysisDbContext(DbContextOptions<AntiPlagAnalysisDbContext> options)
        : base(options)
    {
    }

    public DbSet<AnalysisJob> Jobs => Set<AnalysisJob>();
    public DbSet<AnalysisResult> Results => Set<AnalysisResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnalysisJob>()
            .HasOne(j => j.Result)
            .WithOne(r => r.Job)
            .HasForeignKey<AnalysisResult>(r => r.AnalysisJobId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}