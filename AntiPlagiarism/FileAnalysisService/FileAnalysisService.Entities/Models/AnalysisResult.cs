namespace FileAnalysisService.Entities.Models;

public class AnalysisResult
{
    public Guid Id { get; set; }
    public Guid AnalysisJobId { get; set; }
    
    public Guid WorkId { get; set; }  
    public string FileHash { get; set; } = default!;

    public bool IsDuplicate { get; set; }
    
    public Guid? MatchedWorkId { get; set; }

    public string? WordCloudUrl { get; set; }

    public DateTimeOffset CompletedAt { get; set; }

    public AnalysisJob Job { get; set; } = default!;
}