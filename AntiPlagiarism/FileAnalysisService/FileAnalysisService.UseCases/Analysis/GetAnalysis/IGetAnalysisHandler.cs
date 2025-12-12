namespace FileAnalysisService.UseCases.Analysis.GetAnalysis;

public interface IGetAnalysisHandler
{
    Task<GetAnalysisResponse?> HandleAsync(Guid analysisJobId, CancellationToken token);
}