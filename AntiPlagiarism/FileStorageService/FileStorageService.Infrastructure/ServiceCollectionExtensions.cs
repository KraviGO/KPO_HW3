using FileStorageService.Infrastructure.Data;
using FileStorageService.Infrastructure.Data.Repositories.Works;
using FileStorageService.Infrastructure.Data.Repositories.Files;
using FileStorageService.Infrastructure.FileSystem;
using FileStorageService.UseCases.Works.AddWork;
using FileStorageService.UseCases.Works.GetWork;
using FileStorageService.UseCases.Files.GetFile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorageService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileStorageInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string storagePath)
    {
        services.AddDbContext<AntiPlagiarismDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IAddWorkRepository, EfAddWorkRepository>();

        services.AddSingleton<IFileStorageProvider>(new LocalFileStorageProvider(storagePath));
        
        services.AddScoped<IGetWorkRepository, EfGetWorkRepository>();
        
        services.AddScoped<IGetFileRepository, EfGetFileRepository>();

        return services;
    }
}