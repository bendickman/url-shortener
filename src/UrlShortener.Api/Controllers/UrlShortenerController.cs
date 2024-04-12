using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain.Services.Interfaces;
using UrlShortener.Shared.Models;

namespace UrlShortener.Api.Controllers;

[Route("/api/url-shortener")]
public class UrlShortenerController : Controller
{
    private readonly IUrlShortenerService _urlShortenerService;

    public UrlShortenerController(
        IUrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    [HttpPost]
    public IActionResult Index(
        [FromBody] UrlShortenRequest request)
    {
        var url = request.Url;
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            return BadRequest("Not a valid URL");
        }

        var result = _urlShortenerService.ShortenUrl(url);

        if (string.IsNullOrEmpty(result))
        {
            return BadRequest(url);
        }

        var response = new UrlShortenResponse
        {
            ShortUrl = result,
        };

        return Ok(response);
    }

    [HttpGet, Route("{token}")]
    public IActionResult Redirect(
        [FromRoute] string token)
    {
        var url = _urlShortenerService.GetUrl(token);

        if (string.IsNullOrEmpty(url))
        {
            return NotFound();
        }

        return Ok(url);
    }
}
