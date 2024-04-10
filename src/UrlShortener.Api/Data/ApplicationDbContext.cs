using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Settings;
using UrlShortener.Domain.Models;

namespace UrlShortener.Api.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IApplicationSettings _applicationSettings;

    public ApplicationDbContext(
        DbContextOptions options,
        IApplicationSettings applicationSettings)
        : base(options)
    {
        _applicationSettings = applicationSettings;
    }

    public DbSet<UrlEntity> UrlEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrlEntity>(builder =>
        {
            builder
            .Property(shortenedUrl => shortenedUrl.Token)
            .HasMaxLength(_applicationSettings.UrlShortenedLength);

            builder
            .HasIndex(shortenedUrl => shortenedUrl.Token)
            .IsUnique();
        });
    }
}
