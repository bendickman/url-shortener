namespace UrlShortener.Api.Settings;

public class ApplicationSettings : IApplicationSettings
{
    public string UrlSafeCharacters { get; init; } = string.Empty;

    public int UrlShortenedLength { get; init; }

    public string BaseUrl { get; init; } = string.Empty;
}
