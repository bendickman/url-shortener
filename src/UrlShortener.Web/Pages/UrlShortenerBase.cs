using Microsoft.AspNetCore.Components;
using UrlShortener.Web.Services.Interfaces;

namespace UrlShortener.Web.Pages;

public class UrlShortenerBase : ComponentBase
{
    [Inject]
    public IUrlShortenerService UrlShortenerService { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
}
