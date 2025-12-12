namespace FileStorageService.UseCases.Works.AddWork;

public class AddWorkResponse
{
    public Guid WorkId { get; init; }
    public DateTimeOffset SubmittedAt { get; init; }
    public AddWorkFileInfo File { get; init; } = null!;
}