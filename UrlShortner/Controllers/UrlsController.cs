using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;
using UrlShortner.DTOs;
using UrlShortner.Models;
using RandomString;

namespace UrlShortner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly UrlShortnerContext _context;

        public UrlsController(UrlShortnerContext context)
        {
            _context = context;
        }
        [HttpGet("/byid/{id}")]
        public async Task<ActionResult<Url>> GetUrlById(int id)
        {
            if (id >= 0) return BadRequest("Invalid Id");

            var res = await _context.Urls.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpGet("bykey/{key}")]
        public async Task<ActionResult<Url>> GetUrlByKey(string key)
        {
            if (key == null || key == string.Empty) return BadRequest();
            var res = await _context.Urls.FirstOrDefaultAsync(x  => x.Key == key);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<Url>> NewShortUrl(NewURLDTO newURLDTO)
        {
            if (newURLDTO == null || newURLDTO.Url == string.Empty)
            {
                return BadRequest();
            }
            var exixts = await _context.Urls.FirstOrDefaultAsync(x => x.LongUrl == newURLDTO.Url);
            if (exixts != null)
            {
                return Ok(exixts);
            }
            bool keyExists = true;
            string key;
            key = Hasher.Hmac256ToString(newURLDTO.Url, 8);
            int counter = 0;
            do
            {
                keyExists = await KeyExists(key);
                if (keyExists)
                {
                    key = Hasher.Hmac256ToString(newURLDTO.Url, 8, counter);
                    counter++;
                }
            } while(keyExists);
            if (!newURLDTO.Url.StartsWith("http://") && !newURLDTO.Url.StartsWith("https://")) newURLDTO.Url = $"http://{newURLDTO.Url}";
            Url url = new Url { LongUrl = newURLDTO.Url, Key = key, ShortUrl = $"{newURLDTO.ShortUrl}{key}" };
            _context.Urls.Add(url);
            await _context.SaveChangesAsync();
            return Ok(url);
        }
        [HttpDelete("{sUrl}")]
        public async Task<ActionResult> DeleteByShortUrl(string sUrl)
        {
            var res = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == sUrl);
            if(res == null) return NotFound();
             _context.Entry(res).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private async Task<bool> KeyExists(string key)
        {
            return await _context.Urls.AnyAsync(x => x.Key == key);
        }
    }
}
