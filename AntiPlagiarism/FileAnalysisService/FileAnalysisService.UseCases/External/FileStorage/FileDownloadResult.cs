namespace FileAnalysisService.UseCases.External.FileStorage;

public class FileDownloadResult
{
    public Stream Content { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
}