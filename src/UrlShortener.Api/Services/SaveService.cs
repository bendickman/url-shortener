using UrlShortener.Api.Data;
using UrlShortener.Domain.Models;
using UrlShortener.Domain.Services.Interfaces;

namespace UrlShortener.Api.Services;

public class SaveService : ISaveService
{
    private readonly ApplicationDbContext _dbContext;

    public SaveService(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Save(UrlEntity urlEntity)
    {
        if (!_dbContext.UrlEntities.Any(u => u.Token == urlEntity.Token))
        {
            _dbContext.UrlEntities.Add(urlEntity);
            _dbContext.SaveChanges();
            return true;
        }

        return false;
    }

    public bool Exists(string token)
    {
        return _dbContext.UrlEntities.Any(u => u.Token == token);
    }

    public UrlEntity Get(string token)
    {
        return _dbContext.UrlEntities.FirstOrDefault(u => u.Token == token)!;
    }
}
