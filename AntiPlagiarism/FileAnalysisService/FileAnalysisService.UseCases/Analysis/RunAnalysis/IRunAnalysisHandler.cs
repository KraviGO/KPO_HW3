namespace FileAnalysisService.UseCases.Analysis.RunAnalysis;

public interface IRunAnalysisHandler
{
    Task<RunAnalysisResponse> HandleAsync(RunAnalysisRequest request, CancellationToken token);
}