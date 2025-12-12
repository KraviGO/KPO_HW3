namespace FileStorageService.UseCases.Works.AddWork;

public class AddWorkRequest
{
    public string StudentId { get; init; } = null!;
    public string StudentName { get; init; } = null!;

    public string AssignmentId { get; init; } = null!;
    public string? AssignmentTitle { get; init; }

    public string OriginalFileName { get; init; } = null!;
    public string ContentType { get; init; } = null!;

    public Stream FileContent { get; init; } = null!;
}