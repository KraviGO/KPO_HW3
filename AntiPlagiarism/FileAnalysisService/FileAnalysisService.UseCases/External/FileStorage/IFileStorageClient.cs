namespace FileAnalysisService.UseCases.External.FileStorage;

public interface IFileStorageClient
{
    Task<FileDownloadResult> DownloadFileAsync(Guid fileId, CancellationToken token);
}