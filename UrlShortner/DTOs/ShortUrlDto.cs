namespace UrlShortner.DTOs;

public record NewUrlDto(string Url);

public class ShortUrlDto
{
    public string Url { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
}
