using Microsoft.AspNetCore.Http;

namespace FileStorageService.Presentation.Endpoints;

public class UploadWorkForm
{
    public IFormFile File { get; set; } = null!;
    public string StudentId { get; set; } = null!;
    public string StudentName { get; set; } = null!;
    public string AssignmentId { get; set; } = null!;
    public string? AssignmentTitle { get; set; }
}