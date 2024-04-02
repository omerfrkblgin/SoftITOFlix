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
        public ActionResult<List<MediaActor>> GetMediaActors()
        {
            return _context.MediaActors.ToList();
        }

        // GET: api/MediaActors/5
        [HttpGet("{id}")]
        public ActionResult<MediaActor> GetMediaActor(int mediaId)
        {
            MediaActor mediaActor = _context.MediaActors.Where(m => m.MediaId == mediaId).FirstOrDefault()!;

            if (mediaActor == null)
            {
                return NotFound();
            }

            return mediaActor;
        }

        // PUT: api/MediaActors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public ActionResult PutMediaActor(int id, MediaActor mediaActor)
        //{
        //    if (id != mediaActor.MediaId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(mediaActor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MediaActorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/MediaActors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostMediaActor(string mediaName, string actorName)
        {
            Media? media = _context.Medias.Where(m => m.Name == mediaName).FirstOrDefault()!;
            Actor? actor = _context.Actors.Where(a => a.Name == actorName).FirstOrDefault()!;
            if (media == null || actor == null)
            {
                
            }
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
