namespace FileStorageService.UseCases.Works.GetWork;

public class GetWorkRequestHandler : IGetWorkRequestHandler
{
    private readonly IGetWorkRepository _repository;

    public GetWorkRequestHandler(IGetWorkRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetWorkResponse?> HandleAsync(GetWorkRequest request, CancellationToken cancellationToken)
    {
        var work = await _repository.GetByIdAsync(request.WorkId, cancellationToken);

        if (work is null)
            return null;

        return GetWorkMapper.ToResponse(work);
    }
}