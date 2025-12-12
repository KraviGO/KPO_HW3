using FileStorageService.Entities.Models;
using FileStorageService.Infrastructure.Data;
using FileStorageService.UseCases.Files.GetFile;
using Microsoft.EntityFrameworkCore;

namespace FileStorageService.Infrastructure.Data.Repositories.Files;

public class EfGetFileRepository : IGetFileRepository
{
    private readonly AntiPlagiarismDbContext _dbContext;

    public EfGetFileRepository(AntiPlagiarismDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<StoredFile?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken)
    {
        return _dbContext.Files
            .SingleOrDefaultAsync(f => f.Id == fileId, cancellationToken);
    }
}