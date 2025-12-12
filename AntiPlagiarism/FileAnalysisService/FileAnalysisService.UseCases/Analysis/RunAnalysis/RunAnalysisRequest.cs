namespace FileAnalysisService.UseCases.Analysis.RunAnalysis;

public class RunAnalysisRequest
{
    public Guid WorkId { get; set; }
    public Guid FileId { get; set; }
}