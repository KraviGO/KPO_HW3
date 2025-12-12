namespace FileAnalysisService.UseCases.Analysis.RunAnalysis;

public class RunAnalysisResponse
{
    public Guid AnalysisId { get; set; }
    public bool IsDuplicate { get; set; }
    public Guid? MatchedWorkId { get; set; }
}