using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            MediaActor? mediaActor = _context.MediaActors.Where(m => m.MediaId == mediaId).FirstOrDefault();

            if (mediaActor == null)
            {
                return NotFound();
            }

            return mediaActor;
        }

        

        // POST: api/MediaActors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public bool PostMediaActor(string mediaName, string actorName)
        {
            Media? media = _context.Medias.Where(m => m.Name == mediaName).FirstOrDefault();
            Actor? actor = _context.Actors.Where(a => a.Name == actorName).FirstOrDefault();

            MediaActor? mediaActor = new MediaActor();

            if (media == null || actor == null)
            {
                return false;
            }
            mediaActor.ActorId = actor.Id;
            mediaActor.MediaId = media.Id;
            _context.MediaActors.Add(mediaActor);
            _context.SaveChanges();
            return true;
        }

        // DELETE: api/MediaActors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public ActionResult DeleteMediaActor(int id)
        {
            MediaActor? mediaActor = _context.MediaActors.Find(id);
            if (mediaActor == null)
            {
                return NotFound();
            }

            _context.MediaActors.Remove(mediaActor);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
