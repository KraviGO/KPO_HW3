using FileStorageService.UseCases.Works.AddWork;
using System.Security.Cryptography;

namespace FileStorageService.Infrastructure.FileSystem;

public class LocalFileStorageProvider : IFileStorageProvider
{
    private readonly string _basePath;

    public LocalFileStorageProvider(string basePath)
    {
        _basePath = basePath;

        if (!Directory.Exists(basePath))
            Directory.CreateDirectory(basePath);
    }

    public async Task<FileStorageResult> SaveAsync(
        Stream content,
        string fileName,
        CancellationToken cancellationToken)
    {
        var fileId = Guid.NewGuid();
        var newFileName = $"{fileId}_{fileName}";

        var relativePath = newFileName;

        var fullPath = Path.Combine(_basePath, relativePath);

        await using (var fs = File.Create(fullPath))
        {
            await content.CopyToAsync(fs, cancellationToken);
        }

        long size = new FileInfo(fullPath).Length;
        string hash = ComputeSha256(fullPath);

        return new FileStorageResult
        {
            StoragePath = relativePath,
            Size = size,
            Hash = hash
        };
    }
    
    public Task<Stream> OpenReadAsync(
        string storagePath,
        CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(_basePath, storagePath);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException("Stored file not found", fullPath);

        return Task.FromResult<Stream>(File.OpenRead(fullPath));
    }

    private static string ComputeSha256(string filePath)
    {
        using var sha = SHA256.Create();
        using var stream = File.OpenRead(filePath);
        var hash = sha.ComputeHash(stream);

        return BitConverter
            .ToString(hash)
            .Replace("-", "")
            .ToLowerInvariant();
    }
}