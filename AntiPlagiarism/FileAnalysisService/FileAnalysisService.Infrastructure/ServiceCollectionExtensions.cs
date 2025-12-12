using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileAnalysisService.Infrastructure.Data;
using FileAnalysisService.Infrastructure.Data.Repositories;
using FileAnalysisService.UseCases.Analysis.Common;
using FileAnalysisService.UseCases.External.FileStorage;
using FileAnalysisService.Infrastructure.External.WordCloud;
using FileAnalysisService.Infrastructure.External.FileStorage;
using FileAnalysisService.Infrastructure.TextExtraction;

namespace FileAnalysisService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Database
        services.AddDbContext<AntiPlagAnalysisDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AnalysisDb")));

        // Repositories
        services.AddScoped<IAnalysisJobRepository, EfAnalysisJobRepository>();
        services.AddScoped<IAnalysisResultRepository, EfAnalysisResultRepository>();

        // External Services
        services.AddHttpClient<IFileStorageClient, FileStorageClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["FileStorage:BaseUrl"]);
        });
        
        // TextExtractor
        services.AddScoped<ITextExtractor, TextExtractor>();
        
        // QuickChart
        services.AddHttpClient<IWordCloudGenerator, QuickChartWordCloudGenerator>(client =>
        {
            client.BaseAddress = new Uri("https://quickchart.io");
        });

        return services;
    }
}