using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain.Services.Interfaces;

namespace UrlShortener.Api.Controllers;
public class UrlShortenerController : Controller
{
    private readonly IUrlShortenerService _urlShortenerService;

    public UrlShortenerController(
        IUrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    [HttpPost, Route("/")]
    public IActionResult Index(
        [FromBody] string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            return BadRequest("Not a valid URL");
        }

        var result = _urlShortenerService.ShortenUrl(url);

        if (string.IsNullOrEmpty(result))
        {
            return BadRequest(url);
        }

        return Ok(result);
    }

    [HttpGet, Route("/{token}")]
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
