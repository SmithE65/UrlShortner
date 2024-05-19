using Microsoft.EntityFrameworkCore;
using UrlShortner.Models;

namespace UrlShortner.Data;

public class UrlShortnerContext(DbContextOptions<UrlShortnerContext> options) : DbContext(options)
{
    public DbSet<Url> Urls { get; set; } = default!;
}
