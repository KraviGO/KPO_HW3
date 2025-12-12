using FileAnalysisService.UseCases.Analysis.Common;
using FileAnalysisService.UseCases.Analysis.GetAnalysis;

public class GetAnalysisHandler : IGetAnalysisHandler
{
    private readonly IAnalysisJobRepository _jobRepository;
    private readonly IAnalysisResultRepository _resultRepository;

    public GetAnalysisHandler(
        IAnalysisJobRepository jobRepository,
        IAnalysisResultRepository resultRepository)
    {
        _jobRepository = jobRepository;
        _resultRepository = resultRepository;
    }

    public async Task<GetAnalysisResponse?> HandleAsync(Guid analysisJobId, CancellationToken token)
    {
        var job = await _jobRepository.GetByIdAsync(analysisJobId, token);
        if (job == null)
            return null;

        var result = await _resultRepository.GetByJobIdAsync(analysisJobId, token);
        if (result == null)
            return null;

        return new GetAnalysisResponse
        {
            AnalysisJobId = job.Id,
            WorkId = job.WorkId,
            FileId = job.FileId,

            IsDuplicate = result.IsDuplicate,
            MatchedWorkId = result.MatchedWorkId,
            WordCloudUrl = result.WordCloudUrl,
            CompletedAt = result.CompletedAt
        };
    }
}