namespace UrlShortener.Domain.Services.Interfaces;
public interface ISystemClock
{
    DateTimeOffset UtcNow { get; }
}
