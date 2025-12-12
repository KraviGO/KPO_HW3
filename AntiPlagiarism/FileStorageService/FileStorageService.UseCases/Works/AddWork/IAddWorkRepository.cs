using FileStorageService.Entities.Models;

namespace FileStorageService.UseCases.Works.AddWork;

public interface IAddWorkRepository
{
    Task AddAsync(Work work, StoredFile file, CancellationToken cancellationToken);
}