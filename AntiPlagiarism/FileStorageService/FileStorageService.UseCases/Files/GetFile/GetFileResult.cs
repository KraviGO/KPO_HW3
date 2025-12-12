namespace FileStorageService.UseCases.Files.GetFile;

public class GetFileResult
{
    public Stream Content { get; init; } = default!;
    public string ContentType { get; init; } = default!;
    public string DownloadFileName { get; init; } = default!;
}