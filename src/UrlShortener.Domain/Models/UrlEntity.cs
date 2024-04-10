namespace UrlShortener.Domain.Models;
public class UrlEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string OriginalUrl { get; set; } = string.Empty;

    public string ShortenedUrl { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public DateTimeOffset CreatedDate { get; set; }
}
