namespace FileAnalysisService.Entities.Models;

public class DuplicateInfo
{
    public Guid WorkId { get; set; }
    public string Hash { get; set; } = default!;
}