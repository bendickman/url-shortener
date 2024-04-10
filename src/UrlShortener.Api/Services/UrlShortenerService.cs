using UrlShortener.Api.Settings;
using UrlShortener.Domain.Models;
using UrlShortener.Domain.Services.Interfaces;

namespace UrlShortener.Api.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly IApplicationSettings _applicationSettings;
    private readonly ISaveService _saveService;
    private readonly ISystemClock _systemClock;
    private readonly Random _random = new();
    private readonly int _codeLength;
    private readonly char[] _codeChars;

    public UrlShortenerService(
        IApplicationSettings applicationSettings,
        ISaveService saveService,
        ISystemClock systemClock)
    {
        _applicationSettings = applicationSettings;
        _saveService = saveService;
        _systemClock = systemClock;
        _codeLength = _applicationSettings.UrlShortenedLength;
        _codeChars = new char[_codeLength];
    }

    public string GetUrl(string token)
    {
        var urlEntity = _saveService.Get(token);

        return urlEntity?.OriginalUrl ?? string.Empty;
    }

    public string ShortenUrl(string url)
    {
        var token = GenerateUniqueToken();
        while (_saveService.Exists(token))
        {
            token = GenerateUniqueToken();
        }

        var urlEntity = new UrlEntity
        {
            OriginalUrl = url,
            ShortenedUrl = $"{_applicationSettings.BaseUrl}/{token}",
            Token = token,
            CreatedDate = _systemClock.UtcNow,
        };

        _saveService.Save(urlEntity);

        return urlEntity.ShortenedUrl;
    }

    private string GenerateUniqueToken()
    {
        for (int i = 0; i < _codeLength; i++)
        {
            var index = _random.Next(_applicationSettings.UrlSafeCharacters.Length);
            _codeChars[i] = _applicationSettings.UrlSafeCharacters[index];
        }

        return new string(_codeChars);
    }
}
