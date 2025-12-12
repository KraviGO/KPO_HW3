using FileAnalysisService.Entities.Models;

namespace FileAnalysisService.UseCases.Analysis.Common;

public interface IAnalysisResultRepository
{
    Task<AnalysisResult?> FindByHashAsync(string hash, CancellationToken token);
    Task<AnalysisResult?> GetByJobIdAsync(Guid jobId, CancellationToken token);
    Task AddAsync(AnalysisResult result, CancellationToken token);
    Task SaveChangesAsync(CancellationToken token);
}