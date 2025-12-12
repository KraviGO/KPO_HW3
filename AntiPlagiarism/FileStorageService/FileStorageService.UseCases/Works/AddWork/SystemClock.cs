namespace FileStorageService.UseCases.Works.AddWork;

public class SystemClock : ISystemClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}