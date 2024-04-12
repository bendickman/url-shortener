namespace UrlShortener.Web.Services.Interfaces;

public interface IUrlShortenerService
{
    Task<string> GetShortenedUrlAsync(string url);
}
