namespace FileAnalysisService.UseCases.Analysis.GetAnalysis;

public class GetAnalysisResponse
{
    public Guid AnalysisJobId { get; set; }
    public Guid WorkId { get; set; }
    public Guid FileId { get; set; }

    public bool IsDuplicate { get; set; }
    public Guid? MatchedWorkId { get; set; }

    public string? WordCloudUrl { get; set; }
    public DateTimeOffset CompletedAt { get; set; }
}