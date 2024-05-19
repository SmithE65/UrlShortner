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
        if (id >= 0)
        {
            return BadRequest("Invalid Id");
        }

        var res = await _context.Urls.FirstOrDefaultAsync(x => x.Id == id);
        return res == null ? (ActionResult<Url>)NotFound() : (ActionResult<Url>)Ok(res);
    }

    [HttpGet("bykey/{key}")]
    public async Task<ActionResult<Url>> GetUrlByKey(string key)
    {
        if (key == null || key == string.Empty)
        {
            return BadRequest();
        }

        var res = await _context.Urls.FirstOrDefaultAsync(x => x.Key == key);
        return res == null ? (ActionResult<Url>)NotFound() : (ActionResult<Url>)Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<Url>> NewShortUrl(ShortUrlDto newURLDTO)
    {
        if (newURLDTO == null || newURLDTO.Url == string.Empty)
        {
            return BadRequest();
        }

        var exists = await _context.Urls.FirstOrDefaultAsync(x => x.LongUrl == newURLDTO.Url);
        if (exists != null)
        {
            return Ok(exists);
        }

        var keyExists = true;
        string key;
        key = Hasher.Hmac256ToString(newURLDTO.Url, 8);
        var counter = 0;
        do
        {
            keyExists = await KeyExists(key);
            if (keyExists)
            {
                key = Hasher.Hmac256ToString(newURLDTO.Url, 8, counter);
                counter++;
            }
        } while (keyExists);

        if (!newURLDTO.Url.StartsWith("http://") && !newURLDTO.Url.StartsWith("https://"))
        {
            newURLDTO.Url = $"http://{newURLDTO.Url}";
        }

        var url = new Url { LongUrl = newURLDTO.Url, Key = key, ShortUrl = $"{newURLDTO.ShortUrl}{key}" };
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
