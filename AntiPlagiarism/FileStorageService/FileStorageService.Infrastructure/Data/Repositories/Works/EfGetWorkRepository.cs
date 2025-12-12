using FileStorageService.Entities.Models;
using FileStorageService.Infrastructure.Data;
using FileStorageService.UseCases.Works.GetWork;
using Microsoft.EntityFrameworkCore;

namespace FileStorageService.Infrastructure.Data.Repositories.Works;

public class EfGetWorkRepository : IGetWorkRepository
{
    private readonly AntiPlagiarismDbContext _dbContext;

    public EfGetWorkRepository(AntiPlagiarismDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Work?> GetByIdAsync(Guid workId, CancellationToken cancellationToken)
    {
        return _dbContext.Works
            .Include(w => w.File)
            .SingleOrDefaultAsync(w => w.Id == workId, cancellationToken);
    }
}