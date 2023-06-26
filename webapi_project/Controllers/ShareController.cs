using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_project.Data;
using webapi_project.Models;

namespace webapi_project.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Normal,Share,Guest")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly NoteDbContext _context;

        public ShareController(NoteDbContext context)
        {
            _context = context;
        }

        // GET: api/Share
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Share>>> GetShare()
        {
            if (_context.Share == null)
            {
                return NotFound();
            }
            return await _context.Share.ToListAsync();
        }

        // GET: api/Share/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Share>> GetShare(int id)
        {
            if (_context.Share == null)
            {
                return NotFound();
            }
            var share = await _context.Share.FindAsync(id);

            if (share == null)
            {
                return NotFound();
            }

            return share;
        }

        // PUT: api/Share/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShare(int id, Share share)
        {
            if (id != share.Id)
            {
                return BadRequest();
            }

            _context.Entry(share).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Share
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Share>> PostShare(Share share)
        {
            if (_context.Share == null)
            {
                return Problem("Entity set 'NoteDbContext.Share'  is null.");
            }
            _context.Share.Add(share);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShare", new { id = share.Id }, share);
        }

        // DELETE: api/Share/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShare(int id)
        {
            if (_context.Share == null)
            {
                return NotFound();
            }
            var share = await _context.Share.FindAsync(id);
            if (share == null)
            {
                return NotFound();
            }

            _context.Share.Remove(share);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShareExists(int id)
        {
            return (_context.Share?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
