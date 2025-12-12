using FileAnalysisService.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using FileAnalysisService.UseCases.Analysis.Common;

namespace FileAnalysisService.Infrastructure.Data.Repositories;

public class EfAnalysisResultRepository : IAnalysisResultRepository
{
    private readonly AntiPlagAnalysisDbContext _db;

    public EfAnalysisResultRepository(AntiPlagAnalysisDbContext db)
    {
        _db = db;
    }

    public async Task<AnalysisResult?> FindByHashAsync(string hash, CancellationToken token)
    {
        return await _db.Results
            .Where(r => r.FileHash == hash)
            .FirstOrDefaultAsync(token);
    }
    
    public Task<AnalysisResult?> GetByJobIdAsync(Guid jobId, CancellationToken token)
    {
        return _db.Results
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.AnalysisJobId == jobId, token);
    }

    public Task AddAsync(AnalysisResult result, CancellationToken token)
    {
        _db.Results.Add(result);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken token)
        => _db.SaveChangesAsync(token);
}