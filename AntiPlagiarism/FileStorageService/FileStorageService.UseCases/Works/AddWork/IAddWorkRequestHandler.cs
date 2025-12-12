namespace FileStorageService.UseCases.Works.AddWork;

public interface IAddWorkRequestHandler
{
    Task<AddWorkResponse> HandleAsync(AddWorkRequest request, CancellationToken cancellationToken);
}