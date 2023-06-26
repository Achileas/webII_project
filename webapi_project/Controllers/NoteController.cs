using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using webapi_project.Models;
using webapi_project.Data;

namespace webapi_project.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly NoteDbContext _context;

        public NoteController(NoteDbContext context)
        {
            _context = context;
        }

        // GET: api/Note
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNote()
        {
            if (_context.Note == null)
            {
                return NotFound();
            }
            // return await _context.Note.ToListAsync();
            return new List<Note>{
                new () { Id = 1, Title = "Note 1 title", Content = "Note 1 content" },
                new () { Id = 2, Title = "Note 2 title", Content = "Note 2 content" },
                new () { Id = 3, Title = "Note 3 title", Content = "Note 3 content" },
                new () { Id = 4, Title = "Note 4 title", Content = "Note 4 content" },
                new () { Id = 5, Title = "Note 5 title", Content = "Note 5 content" },
            };
        }

        // GET: api/Note/5
        // [HttpGet("{name}/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            // if (_context.Note == null)
            // {
            //     return NotFound();
            // }
            // var note = await _context.Note.FindAsync(id);

            // if (note == null)
            // {
            //     return NotFound();
            // }

            // return note;

            return new Note() { Id = 2, Title = "Note 2 title", Content = "Note 2 content", AuthorId = "8b121879-0f2d-41ce-a501-21488b3542d9", ByteLength = 23, WordCount = 100 };
        }

        // PUT: api/Note/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Note
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            if (_context.Note == null)
            {
                return Problem("Entity set 'NoteDbContext.Note' is null.");
            }
            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Note/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (_context.Note == null)
            {
                return NotFound();
            }
            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return (_context.Note?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
