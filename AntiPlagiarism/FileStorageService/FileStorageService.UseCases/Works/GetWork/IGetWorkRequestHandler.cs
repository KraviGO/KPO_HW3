namespace FileStorageService.UseCases.Works.GetWork;

public interface IGetWorkRequestHandler
{
    Task<GetWorkResponse?> HandleAsync(GetWorkRequest request, CancellationToken cancellationToken);
}