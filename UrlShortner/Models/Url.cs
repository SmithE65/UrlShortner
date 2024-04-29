using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace UrlShortner.Models
{
    [Index(nameof(ShortUrl), nameof(Key), IsUnique = true)]
    public class Url
    {
        public int Id { get; set; }
        [StringLength(5)]
        public string Key { get; set; } = string.Empty;
        public string LongUrl { get; set; } = string.Empty;
        public string ShortUrl {  get; set; } = string.Empty;

    }
}
