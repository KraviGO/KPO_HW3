using FileAnalysisService.Entities.Models;
using FileAnalysisService.UseCases.Analysis.Common;
using Microsoft.EntityFrameworkCore;

namespace FileAnalysisService.Infrastructure.Data.Repositories;

public class EfAnalysisJobRepository : IAnalysisJobRepository
{
    private readonly AntiPlagAnalysisDbContext _db;

    public EfAnalysisJobRepository(AntiPlagAnalysisDbContext db)
    {
        _db = db;
    }

    public Task AddAsync(AnalysisJob job, CancellationToken token)
    {
        _db.Jobs.Add(job);
        return Task.CompletedTask;
    }
    
    public Task<AnalysisJob?> GetByIdAsync(Guid jobId, CancellationToken token)
    {
        return _db.Jobs
            .AsNoTracking()
            .FirstOrDefaultAsync(j => j.Id == jobId, token);
    }

    public Task SaveChangesAsync(CancellationToken token)
        => _db.SaveChangesAsync(token);
}