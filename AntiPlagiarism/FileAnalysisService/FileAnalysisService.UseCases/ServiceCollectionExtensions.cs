using Microsoft.Extensions.DependencyInjection;
using FileAnalysisService.UseCases.Analysis.RunAnalysis;
using FileAnalysisService.UseCases.Analysis.GetAnalysis;

namespace FileAnalysisService.UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRunAnalysisHandler, RunAnalysisHandler>();
        services.AddScoped<IGetAnalysisHandler, GetAnalysisHandler>();

        return services;
    }
}