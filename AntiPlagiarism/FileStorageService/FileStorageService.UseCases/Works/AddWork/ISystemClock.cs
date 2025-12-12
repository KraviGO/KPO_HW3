namespace FileStorageService.UseCases.Works.AddWork;

public interface ISystemClock
{
    DateTimeOffset UtcNow { get; }
}