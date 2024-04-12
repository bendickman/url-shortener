using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UrlShortener.Web;
using UrlShortener.Web.Services;
using UrlShortener.Web.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
builder.Services.AddHttpClient<IUrlShortenerService, UrlShortenerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7109/");
});

await builder.Build().RunAsync();
