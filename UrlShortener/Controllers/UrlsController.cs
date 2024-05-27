using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreatedDto>> NewShortUrl(CreateDto dto)
    {
        var url = dto.Url;
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(url));
        var key = Base64UrlEncoder.Encode(hash[..8]);
        var counter = 0;

        var match = _context.Urls.FirstOrDefault(x => x.Key == key || x.LongUrl == url);
        if (match is not null)
        {
            return Ok(new CreatedDto(match.Key, match.LongUrl, match.ShortUrl));
        }

        do
        {
            hash = SHA256.HashData(Encoding.UTF8.GetBytes(url + counter.ToString()));
            key = Base64UrlEncoder.Encode(hash[..8]);
            counter++;
        } while (await _context.Urls.AnyAsync(x => x.Key == key || x.LongUrl == url));

        var newUrl = new Url { LongUrl = url, Key = key, ShortUrl = key };
        _ = _context.Urls.Add(newUrl);
        _ = await _context.SaveChangesAsync();

        return Created(key, newUrl);
    }
}
