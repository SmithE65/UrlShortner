using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;
using UrlShortner.Models;

namespace UrlShortner.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UrlController : Controller
    {
        private readonly UrlShortnerContext _context;

        public UrlController(UrlShortnerContext context)
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
    }
}
