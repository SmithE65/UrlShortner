using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomString;
using UrlShortener.Data;
using UrlShortener.DTOs;
using UrlShortener.Models;

namespace UrlShortener.Controllers;

[Route("/")]
[ApiController]
public class UrlsController(UrlShortenerContext context) : ControllerBase
{
    private readonly UrlShortenerContext _context = context;

    [HttpGet("{key}")]
    public async Task<ActionResult> GetUrlByKey(string key)
    {
        var res = await _context.Urls.FirstOrDefaultAsync(x => x.Key == key);

        return res switch
        {
            null => NotFound(),
            _ => Redirect(res.LongUrl)
        };
    }

    [HttpPost]
    public async Task<ActionResult<CreatedDto>> NewShortUrl(CreateDto dto)
    {
        var url = dto.Url;
        var key = Hasher.Hmac256ToString(url, 8);
        var counter = 0;

        var match = _context.Urls.FirstOrDefault(x => x.Key == key);
        if (match is not null && match.LongUrl == dto.Url)
        {
            return Ok(new CreatedDto(match.Key, match.LongUrl, match.ShortUrl));
        }

        do
        {
            key = Hasher.Hmac256ToString(url, 8, counter);
            counter++;
        } while (await _context.Urls.AnyAsync(x => x.Key == key));

        var newUrl = new Url { LongUrl = url, Key = key, ShortUrl = key };
        _ = _context.Urls.Add(newUrl);
        _ = await _context.SaveChangesAsync();

        return Ok(url);
    }
}
