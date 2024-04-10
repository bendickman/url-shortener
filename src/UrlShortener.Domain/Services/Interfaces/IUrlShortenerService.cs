namespace UrlShortener.Domain.Services.Interfaces;
public interface IUrlShortenerService
{
    string ShortenUrl(string url);

    string GetUrl(string token);
}
