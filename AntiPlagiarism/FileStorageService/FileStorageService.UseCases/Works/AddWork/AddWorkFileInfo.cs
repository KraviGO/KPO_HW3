namespace FileStorageService.UseCases.Works.AddWork;

public class AddWorkFileInfo
{
    public Guid FileId { get; init; }
    public string OriginalFileName { get; init; } = null!;
    public string ContentType { get; init; } = null!;
    public long Size { get; init; }
}