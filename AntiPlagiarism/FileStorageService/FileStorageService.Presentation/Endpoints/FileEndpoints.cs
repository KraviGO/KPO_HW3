using FileStorageService.UseCases.Files.GetFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace FileStorageService.Presentation.Endpoints;

public static class FileEndpoints
{
    public static IEndpointRouteBuilder MapFileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/files");

        group.MapGet("/{id:guid}", async (
                Guid id,
                IGetFileRequestHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(id, cancellationToken);

                if (result is null)
                    return Results.NotFound();
                
                return Results.File(
                    result.Content,
                    contentType: result.ContentType,
                    fileDownloadName: result.DownloadFileName);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}