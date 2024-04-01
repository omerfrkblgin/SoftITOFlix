using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Data;
using SoftITOFlix.Models;

namespace SoftITOFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaActorsController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public MediaActorsController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/MediaActors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaActor>>> GetMediaActors()
        {
          if (_context.MediaActors == null)
          {
              return NotFound();
          }
            return await _context.MediaActors.ToListAsync();
        }

        // GET: api/MediaActors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaActor>> GetMediaActor(int id)
        {
          if (_context.MediaActors == null)
          {
              return NotFound();
          }
            var mediaActor = await _context.MediaActors.FindAsync(id);

            if (mediaActor == null)
            {
                return NotFound();
            }

            return mediaActor;
        }

        // PUT: api/MediaActors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMediaActor(int id, MediaActor mediaActor)
        {
            if (id != mediaActor.MediaId)
            {
                return BadRequest();
            }

            _context.Entry(mediaActor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaActorExists(id))
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

        // POST: api/MediaActors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MediaActor>> PostMediaActor(MediaActor mediaActor)
        {
          if (_context.MediaActors == null)
          {
              return Problem("Entity set 'SoftITOFlixContext.MediaActors'  is null.");
          }
            _context.MediaActors.Add(mediaActor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MediaActorExists(mediaActor.MediaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMediaActor", new { id = mediaActor.MediaId }, mediaActor);
        }

        // DELETE: api/MediaActors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMediaActor(int id)
        {
            if (_context.MediaActors == null)
            {
                return NotFound();
            }
            var mediaActor = await _context.MediaActors.FindAsync(id);
            if (mediaActor == null)
            {
                return NotFound();
            }

            _context.MediaActors.Remove(mediaActor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MediaActorExists(int id)
        {
            return (_context.MediaActors?.Any(e => e.MediaId == id)).GetValueOrDefault();
        }
    }
}
