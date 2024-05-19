using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomString;
using UrlShortner.Data;
using UrlShortner.DTOs;
using UrlShortner.Models;

namespace UrlShortner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlsController(UrlShortnerContext context) : ControllerBase
{
    private readonly UrlShortnerContext _context = context;

    [HttpGet("/byid/{id}")]
    public async Task<ActionResult<Url>> GetUrlById(int id)
    {
        var res = await _context.Urls.FirstOrDefaultAsync(x => x.Id == id);

        return res switch
        {
            null => NotFound(),
            _ => Ok(res)
        };
    }

    [HttpGet("{key}")]
    public async Task<ActionResult<Url>> GetUrlByKey(string key)
    {
        var res = await _context.Urls.FirstOrDefaultAsync(x => x.Key == key);

        return res switch
        {
            null => NotFound(),
            _ => Ok(res)
        };
    }

    [HttpPost]
    public async Task<ActionResult<ShortUrlDto>> NewShortUrl(ShortUrlDto dto)
    {
        var exists = await _context.Urls.FirstOrDefaultAsync(x => x.LongUrl == dto.Url);

        if (exists != null)
        {
            return Ok(exists);
        }

        var keyExists = true;
        string key;
        key = Hasher.Hmac256ToString(dto.Url, 8);
        var counter = 0;

        do
        {
            keyExists = await KeyExists(key);

            if (keyExists)
            {
                key = Hasher.Hmac256ToString(dto.Url, 8, counter);
                counter++;
            }
        } while (keyExists);

        var url = new Url { LongUrl = dto.Url, Key = key, ShortUrl = $"{dto.ShortUrl}{key}" };
        _ = _context.Urls.Add(url);
        _ = await _context.SaveChangesAsync();

        return Ok(url);
    }

    [HttpDelete("{sUrl}")]
    public async Task<ActionResult> DeleteByShortUrl(string sUrl)
    {
        var res = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == sUrl);
        if (res == null)
        {
            return NotFound();
        }

        _context.Entry(res).State = EntityState.Deleted;
        _ = await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> KeyExists(string key)
    {
        return await _context.Urls.AnyAsync(x => x.Key == key);
    }
}
