using FileStorageService.UseCases.Works.AddWork;

namespace FileStorageService.UseCases.Files.GetFile;

public class GetFileRequestHandler : IGetFileRequestHandler
{
    private readonly IGetFileRepository _repository;
    private readonly IFileStorageProvider _fileStorageProvider;

    public GetFileRequestHandler(
        IGetFileRepository repository,
        IFileStorageProvider fileStorageProvider)
    {
        _repository = repository;
        _fileStorageProvider = fileStorageProvider;
    }

    public async Task<GetFileResult?> HandleAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(fileId, cancellationToken);

        if (file is null)
            return null;

        var stream = await _fileStorageProvider.OpenReadAsync(file.StoragePath, cancellationToken);

        return new GetFileResult
        {
            Content = stream,
            ContentType = file.ContentType,
            DownloadFileName = file.OriginalFileName
        };
    }
}