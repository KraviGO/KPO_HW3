using FileStorageService.Entities.Models;

namespace FileStorageService.UseCases.Files.GetFile;

public interface IGetFileRepository
{
    Task<StoredFile?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken);
}