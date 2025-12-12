namespace FileStorageService.UseCases.Works.GetWork;

public class GetWorkResponse
{
    public Guid WorkId { get; set; }
    public DateTimeOffset SubmittedAt { get; set; }

    public string StudentId { get; set; } = default!;
    public string StudentName { get; set; } = default!;
    public string AssignmentId { get; set; } = default!;
    public string? AssignmentTitle { get; set; }

    public GetWorkFileInfo File { get; set; } = default!;
}