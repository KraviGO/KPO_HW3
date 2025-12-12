using FileAnalysisService.UseCases.Analysis.Common;
using FileAnalysisService.UseCases.Analysis.RunAnalysis;
using FileAnalysisService.UseCases.Analysis.GetAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace FileAnalysisService.Presentation.Endpoints;

public static class AnalysisEndpoints
{
    public static IEndpointRouteBuilder MapAnalysisEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/analysis");

        // POST /analysis/run
        group.MapPost("/run", async (
                [FromBody] RunAnalysisRequest request,
                IRunAnalysisHandler handler,
                CancellationToken token) =>
            {
                var response = await handler.HandleAsync(request, token);
                return Results.Ok(response);
            })
            .Produces<RunAnalysisResponse>(StatusCodes.Status200OK);

        // GET /analysis/{id}
        group.MapGet("/{id:guid}", async (
                Guid id,
                IGetAnalysisHandler handler,
                CancellationToken token) =>
            {
                var result = await handler.HandleAsync(id, token);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            })
            .Produces<GetAnalysisResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}