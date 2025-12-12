using FileAnalysisService.Entities.Models;

namespace FileAnalysisService.UseCases.Analysis.Common;

public interface IAnalysisJobRepository
{
    Task AddAsync(AnalysisJob job, CancellationToken token);
    Task<AnalysisJob?> GetByIdAsync(Guid jobId, CancellationToken token);
    Task SaveChangesAsync(CancellationToken token);
}