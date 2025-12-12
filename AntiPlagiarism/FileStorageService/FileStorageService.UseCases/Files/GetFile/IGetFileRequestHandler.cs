namespace FileStorageService.UseCases.Files.GetFile;

public interface IGetFileRequestHandler
{
    Task<GetFileResult?> HandleAsync(Guid fileId, CancellationToken cancellationToken);
}