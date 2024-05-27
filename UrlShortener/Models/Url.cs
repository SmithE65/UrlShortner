using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models;

[Index(nameof(ShortUrl), nameof(Key), IsUnique = true)]
public class Url
{
    public int Id { get; set; }

    [Required]
    [MaxLength(8)]
    public required string Key { get; set; } = string.Empty;

    [Required]
    [MaxLength(8000)]
    public required string LongUrl { get; set; } = string.Empty;

    [Required]
    [MaxLength(128)]
    public required string ShortUrl { get; set; } = string.Empty;
}
