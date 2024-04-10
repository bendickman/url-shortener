namespace UrlShortener.Api.Settings;

public interface IApplicationSettings
{
    string UrlSafeCharacters { get; init; }

    int UrlShortenedLength { get; init; }

    string BaseUrl { get; init; }
}