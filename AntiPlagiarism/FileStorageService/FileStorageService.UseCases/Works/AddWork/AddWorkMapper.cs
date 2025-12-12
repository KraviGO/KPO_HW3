using FileStorageService.Entities.Models;

namespace FileStorageService.UseCases.Works.AddWork;

public static class AddWorkMapper
{
    public static StoredFile ToStoredFile(AddWorkRequest request, FileStorageResult storage)
    {
        return new StoredFile
        {
            Id = Guid.NewGuid(),
            OriginalFileName = request.OriginalFileName,
            ContentType = request.ContentType,
            Size = storage.Size,
            StoragePath = storage.StoragePath,
            Hash = storage.Hash
        };
    }

    public static Work ToWork(AddWorkRequest request, StoredFile file, DateTimeOffset submittedAt)
    {
        return new Work
        {
            Id = Guid.NewGuid(),
            StudentId = request.StudentId,
            StudentName = request.StudentName,
            AssignmentId = request.AssignmentId,
            AssignmentTitle = request.AssignmentTitle,
            SubmittedAt = submittedAt,
            FileId = file.Id,
            File = file
        };
    }

    public static AddWorkResponse ToResponse(Work work, StoredFile file)
    {
        return new AddWorkResponse
        {
            WorkId = work.Id,
            SubmittedAt = work.SubmittedAt,
            File = new AddWorkFileInfo
            {
                FileId = file.Id,
                OriginalFileName = file.OriginalFileName,
                ContentType = file.ContentType,
                Size = file.Size
            }
        };
    }
}