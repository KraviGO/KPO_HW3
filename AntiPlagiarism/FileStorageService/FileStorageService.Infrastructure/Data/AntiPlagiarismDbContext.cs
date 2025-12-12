using FileStorageService.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorageService.Infrastructure.Data;

public class AntiPlagiarismDbContext : DbContext
{
    public AntiPlagiarismDbContext(DbContextOptions<AntiPlagiarismDbContext> options)
        : base(options)
    {
    }

    public DbSet<Work> Works => Set<Work>();
    public DbSet<StoredFile> Files => Set<StoredFile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Work>(builder =>
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.File)
                .WithOne(f => f.Work)
                .HasForeignKey<Work>(w => w.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StoredFile>(builder =>
        {
            builder.HasKey(f => f.Id);
        });
    }
}