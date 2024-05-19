using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortner.Models;

[Index(nameof(ShortUrl), nameof(Key), IsUnique = true)]
public class Url
{
    public int Id { get; set; }

    [Required]
    [MaxLength(8)]
    public string Key { get; set; } = string.Empty;

    [Required]
    [MaxLength(8000)]
    public string LongUrl { get; set; } = string.Empty;

    [Required]
    [MaxLength(128)]
    public string ShortUrl { get; set; } = string.Empty;
}
