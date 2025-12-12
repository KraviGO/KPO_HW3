using FileStorageService.Entities.Models;

namespace FileStorageService.UseCases.Works.GetWork;

public static class GetWorkMapper
{
    public static GetWorkResponse ToResponse(Work work)
    {
        if (work.File is null)
            throw new InvalidOperationException("Work.File must be included");

        return new GetWorkResponse
        {
            WorkId = work.Id,
            SubmittedAt = work.SubmittedAt,
            StudentId = work.StudentId,
            StudentName = work.StudentName,
            AssignmentId = work.AssignmentId,
            AssignmentTitle = work.AssignmentTitle,
            File = new GetWorkFileInfo
            {
                FileId = work.File.Id,
                OriginalFileName = work.File.OriginalFileName,
                ContentType = work.File.ContentType,
                Size = work.File.Size
            }
        };
    }
}