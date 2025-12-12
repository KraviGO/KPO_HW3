namespace FileStorageService.UseCases.Works.GetWork;

public class GetWorkFileInfo
{
    public Guid FileId { get; set; }
    public string OriginalFileName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long Size { get; set; }
}