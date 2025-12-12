namespace FileAnalysisService.UseCases.Analysis.Common;

public interface IWordCloudGenerator
{
    Task<string?> GenerateAsync(Stream content, CancellationToken token);
}