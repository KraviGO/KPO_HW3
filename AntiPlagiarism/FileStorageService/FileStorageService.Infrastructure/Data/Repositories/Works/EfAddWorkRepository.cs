using FileStorageService.Entities.Models;
using FileStorageService.UseCases.Works.AddWork;

namespace FileStorageService.Infrastructure.Data.Repositories.Works;

public class EfAddWorkRepository : IAddWorkRepository
{
    private readonly AntiPlagiarismDbContext _db;

    public EfAddWorkRepository(AntiPlagiarismDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Work work, StoredFile file, CancellationToken cancellationToken)
    {
        await _db.Files.AddAsync(file, cancellationToken);
        await _db.Works.AddAsync(work, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);
    }
}