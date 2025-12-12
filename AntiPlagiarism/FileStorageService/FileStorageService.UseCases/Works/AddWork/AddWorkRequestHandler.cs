namespace FileStorageService.UseCases.Works.AddWork;

public class AddWorkRequestHandler : IAddWorkRequestHandler
{
    private readonly IAddWorkRepository _repository;
    private readonly IFileStorageProvider _fileStorage;
    private readonly ISystemClock _clock;

    public AddWorkRequestHandler(
        IAddWorkRepository repository,
        IFileStorageProvider fileStorage,
        ISystemClock clock)
    {
        _repository = repository;
        _fileStorage = fileStorage;
        _clock = clock;
    }

    public async Task<AddWorkResponse> HandleAsync(AddWorkRequest request, CancellationToken cancellationToken)
    {
        var storageResult = await _fileStorage.SaveAsync(
            request.FileContent,
            request.OriginalFileName,
            cancellationToken);

        var storedFile = AddWorkMapper.ToStoredFile(request, storageResult);
        var submittedAt = _clock.UtcNow;
        var work = AddWorkMapper.ToWork(request, storedFile, submittedAt);

        await _repository.AddAsync(work, storedFile, cancellationToken);

        return AddWorkMapper.ToResponse(work, storedFile);
    }
}