using FileAnalysisService.UseCases.External.FileStorage;

namespace FileAnalysisService.Infrastructure.External.FileStorage;

public class FileStorageClient : IFileStorageClient
{
    private readonly HttpClient _client;

    public FileStorageClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<FileDownloadResult> DownloadFileAsync(Guid fileId, CancellationToken token)
    {
        var response = await _client.GetAsync($"/files/{fileId}", token);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync(token);

        return new FileDownloadResult
        {
            Content = stream,
            FileName = response.Content.Headers.ContentDisposition?.FileName ?? "file.bin",
            ContentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream"
        };
    }
}