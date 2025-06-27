using Microsoft.AspNetCore.Mvc;
using BackendApi.Models;
using BackendApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuotesController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetQuotes()
        {
            var quotes = await _context.Quotes.ToListAsync();
            return Ok(quotes);
        }

       
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            return Ok(quote);
        }

       
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddQuote([FromBody] Quote newQuote)
        {
            _context.Quotes.Add(newQuote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuote), new { id = newQuote.Id }, newQuote);
        }
    }
}
