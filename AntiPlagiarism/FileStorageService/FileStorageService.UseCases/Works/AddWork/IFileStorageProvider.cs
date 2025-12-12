namespace FileStorageService.UseCases.Works.AddWork;

public interface IFileStorageProvider
{
    Task<FileStorageResult> SaveAsync(
        Stream content,
        string fileName,
        CancellationToken cancellationToken);
    
    Task<Stream> OpenReadAsync(
        string storagePath,
        CancellationToken cancellationToken);
}

public class FileStorageResult
{
    public string StoragePath { get; init; } = null!;
    public long Size { get; init; }
    public string Hash { get; init; } = null!;
}