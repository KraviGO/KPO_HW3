using FileStorageService.UseCases.Works.AddWork;
using FileStorageService.UseCases.Works.GetWork;
using FileStorageService.UseCases.Files.GetFile;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorageService.UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileStorageUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddWorkRequestHandler, AddWorkRequestHandler>();
        services.AddScoped<IGetWorkRequestHandler, GetWorkRequestHandler>();
        services.AddScoped<IGetFileRequestHandler, GetFileRequestHandler>();
        services.AddSingleton<ISystemClock, SystemClock>();

        return services;
    }
}