using UrlShortener.Domain.Models;

namespace UrlShortener.Domain.Services.Interfaces;
public interface ISaveService
{
    bool Save(UrlEntity urlEntity);

    UrlEntity Get(string token);

    bool Exists(string token);
}
