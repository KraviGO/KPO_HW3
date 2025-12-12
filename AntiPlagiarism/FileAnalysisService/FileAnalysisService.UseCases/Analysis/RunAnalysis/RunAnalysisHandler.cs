using System.Security.Cryptography;
using FileAnalysisService.Entities.Models;
using FileAnalysisService.UseCases.Analysis.Common;
using FileAnalysisService.UseCases.External.FileStorage;

namespace FileAnalysisService.UseCases.Analysis.RunAnalysis;

public class RunAnalysisHandler : IRunAnalysisHandler
{
    private readonly IFileStorageClient _fileStorageClient;
    private readonly IAnalysisJobRepository _jobRepository;
    private readonly IAnalysisResultRepository _resultRepository;
    private readonly IWordCloudGenerator _wordCloudGenerator;
    
    public RunAnalysisHandler(
        IFileStorageClient fileStorageClient,
        IAnalysisJobRepository jobRepository,
        IAnalysisResultRepository resultRepository,
        IWordCloudGenerator wordCloudGenerator)
    {
        _fileStorageClient = fileStorageClient;
        _jobRepository = jobRepository;
        _resultRepository = resultRepository;
        _wordCloudGenerator = wordCloudGenerator;
    }

    public async Task<RunAnalysisResponse> HandleAsync(RunAnalysisRequest request, CancellationToken token)
    {
        var file = await _fileStorageClient.DownloadFileAsync(request.FileId, token);
        
        var ms = new MemoryStream();
        await file.Content.CopyToAsync(ms, token);
        ms.Position = 0;

        var hash = ComputeSha256(ms);
        
        var existing = await _resultRepository.FindByHashAsync(hash, token);

        bool isDuplicate = existing is not null;
        Guid? matchedWorkId = existing?.WorkId;
        
        string? wordCloudUrl = null;
        try
        {
            ms.Position = 0;
            wordCloudUrl = await _wordCloudGenerator.GenerateAsync(ms, token);
        }
        catch { }
        
        var job = new AnalysisJob
        {
            Id = Guid.NewGuid(),
            WorkId = request.WorkId,
            FileId = request.FileId,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _jobRepository.AddAsync(job, token);
        
        var result = new AnalysisResult
        {
            Id = Guid.NewGuid(),
            AnalysisJobId = job.Id,
            WorkId = request.WorkId,
            FileHash = hash,
            IsDuplicate = isDuplicate,
            MatchedWorkId = matchedWorkId,
            WordCloudUrl = wordCloudUrl,
            CompletedAt = DateTimeOffset.UtcNow
        };

        await _resultRepository.AddAsync(result, token);
        await _resultRepository.SaveChangesAsync(token);
        
        return new RunAnalysisResponse
        {
            AnalysisId = job.Id,
            IsDuplicate = isDuplicate,
            MatchedWorkId = matchedWorkId
        };
    }

    private static string ComputeSha256(Stream stream)
    {
        stream.Position = 0;
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}