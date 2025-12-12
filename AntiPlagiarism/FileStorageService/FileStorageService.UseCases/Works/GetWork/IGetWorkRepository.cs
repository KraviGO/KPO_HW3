using FileStorageService.Entities.Models;

namespace FileStorageService.UseCases.Works.GetWork;

public interface IGetWorkRepository
{
    Task<Work?> GetByIdAsync(Guid workId, CancellationToken cancellationToken);
}