namespace FileAnalysisService.Entities.Models;

public class AnalysisJob
{
    public Guid Id { get; set; }
    public Guid WorkId { get; set; }
    public Guid FileId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public AnalysisResult? Result { get; set; }
}