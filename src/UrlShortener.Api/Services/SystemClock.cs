using UrlShortener.Domain.Services.Interfaces;

namespace UrlShortener.Api.Services;

public class SystemClock : ISystemClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
