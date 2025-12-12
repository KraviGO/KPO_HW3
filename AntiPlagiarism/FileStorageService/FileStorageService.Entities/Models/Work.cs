namespace FileStorageService.Entities.Models;

public class Work
{
    public Guid Id { get; set; }

    public string StudentId { get; set; } = null!;
    public string StudentName { get; set; } = null!;

    public string AssignmentId { get; set; } = null!;
    public string? AssignmentTitle { get; set; }

    public DateTimeOffset SubmittedAt { get; set; }

    public Guid FileId { get; set; }
    public StoredFile File { get; set; } = null!;
}