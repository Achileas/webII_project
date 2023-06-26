using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_project.Data;

namespace webapi_project.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Normal,Share,Guest")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NoteDbContext _context;

        public NotificationController(NoteDbContext context)
        {
            _context = context;
        }

        // GET: api/Notification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            if (_context.Notification == null)
            {
                return NotFound();
            }
            return await _context.Notification.ToListAsync();
        }

        // GET: api/Notification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            if (_context.Notification == null)
            {
                return NotFound();
            }
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // PUT: api/Notification/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id, Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notification
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            if (_context.Notification == null)
            {
                return Problem("Entity set 'NoteDbContext.Notification'  is null.");
            }
            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
        }

        // DELETE: api/Notification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            if (_context.Notification == null)
            {
                return NotFound();
            }
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return (_context.Notification?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
