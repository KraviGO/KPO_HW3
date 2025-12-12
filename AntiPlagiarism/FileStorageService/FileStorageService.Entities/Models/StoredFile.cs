namespace FileStorageService.Entities.Models;

public class StoredFile
{
    public Guid Id { get; set; }

    public string OriginalFileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long Size { get; set; }

    public string StoragePath { get; set; } = null!;
    public string Hash { get; set; } = null!;

    public Work Work { get; set; } = null!;
}