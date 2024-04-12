using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using UrlShortener.Shared.Models;
using UrlShortener.Web.Services.Interfaces;

namespace UrlShortener.Web.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly HttpClient _httpClient;

    public UrlShortenerService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetShortenedUrlAsync(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/url-shortener");

        var urlShortenRequest = new UrlShortenRequest
        {
            Url = url,
        };

        request.Content = new StringContent(
            JsonConvert.SerializeObject(urlShortenRequest),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
        }

        var result = await response.Content.ReadFromJsonAsync<UrlShortenResponse>();

        return result?.ShortUrl ?? string.Empty;
    }
}
