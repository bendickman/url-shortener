using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using UrlShortener.Api.Data;
using UrlShortener.Api.Services;
using UrlShortener.Api.Settings;
using UrlShortener.Domain.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
builder.Services.AddSingleton<IApplicationSettings>(s => s.GetRequiredService<IOptions<ApplicationSettings>>().Value);

builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
builder.Services.AddScoped<ISystemClock, SystemClock>();
builder.Services.AddScoped<ISaveService, SaveService>();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlite(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7142", "https://localhost:7142")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
    );

app.MapControllers();

app.Run();
