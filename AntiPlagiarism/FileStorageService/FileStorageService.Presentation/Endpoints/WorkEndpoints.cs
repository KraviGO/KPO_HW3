using FileStorageService.UseCases.Works.AddWork;
using FileStorageService.UseCases.Works.GetWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace FileStorageService.Presentation.Endpoints;

public static class WorkEndpoints
{
    public static IEndpointRouteBuilder MapWorkEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/works");

        group.MapPost("/", async (
            HttpRequest request,
            IAddWorkRequestHandler handler,
            CancellationToken cancellationToken) =>
        {
            if (!request.HasFormContentType)
                return Results.BadRequest("Content-Type must be multipart/form-data");

            var form = await request.ReadFormAsync(cancellationToken);
            
            var file = form.Files.GetFile("file");
            if (file is null)
                return Results.BadRequest("Missing file in form-data");
            
            var studentId = form["studentId"].ToString();
            var studentName = form["studentName"].ToString();
            var assignmentId = form["assignmentId"].ToString();
            var assignmentTitle = form["assignmentTitle"].ToString();

            if (string.IsNullOrWhiteSpace(studentId) ||
                string.IsNullOrWhiteSpace(studentName) ||
                string.IsNullOrWhiteSpace(assignmentId))
            {
                return Results.BadRequest("Missing required metadata fields");
            }

            await using var fileStream = file.OpenReadStream();

            var addWorkRequest = new AddWorkRequest
            {
                StudentId = studentId,
                StudentName = studentName,
                AssignmentId = assignmentId,
                AssignmentTitle = assignmentTitle,
                OriginalFileName = file.FileName,
                ContentType = file.ContentType,
                FileContent = fileStream
            };

            var response = await handler.HandleAsync(addWorkRequest, cancellationToken);

            return Results.Ok(response);
        })
        .Accepts<UploadWorkForm>("multipart/form-data")
        .Produces<AddWorkResponse>(StatusCodes.Status200OK);
        
        group.MapGet("/{id:guid}", async (
                Guid id,
                IGetWorkRequestHandler handler,
                CancellationToken cancellationToken) =>
            {
                var request = new GetWorkRequest { WorkId = id };
                var response = await handler.HandleAsync(request, cancellationToken);

                if (response is null)
                    return Results.NotFound();

                return Results.Ok(response);
            })
            .Produces<GetWorkResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}