namespace FileAnalysisService.Infrastructure.TextExtraction;

public interface ITextExtractor
{
    Task<string> ExtractAsync(string fileName, Stream content, CancellationToken token);
}