using System;
using System.Collections.Generic;
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
    public class MediaDirectorsController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public MediaDirectorsController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/MediaDirectors
        [HttpGet]
        public ActionResult<List<MediaDirector>> GetMediaDirectors()
        {
            return _context.MediaDirectors.AsNoTracking().ToList();
        }

        // GET: api/MediaDirectors/5
        [HttpGet("{id}")]
        public ActionResult<MediaDirector> GetMediaDirector(int mediaId)
        {
            
            MediaDirector? mediaDirector = _context.MediaDirectors.Where(m => m.MediaId == mediaId).FirstOrDefault();
            return mediaDirector;
        }

        // POST: api/MediaDirectors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public bool PostMediaDirector(string mediaName, int directorId)
        {
            MediaDirector? mediaDirector = new MediaDirector();
            Media? media = _context.Medias.Where(m => m.Name == mediaName).FirstOrDefault();
            if (media == null)
            {
                return false;
            }
            mediaDirector.MediaId = media.Id;
            mediaDirector.DirectorId = directorId;
            _context.MediaDirectors.Add(mediaDirector);
            _context.SaveChanges();
            return true;
        }

        // DELETE: api/MediaDirectors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public ActionResult DeleteMediaDirector(int mediaId, int directorId)
        {
           
            MediaDirector? mediaDirector = _context.MediaDirectors.Where(m => m.MediaId == mediaId).FirstOrDefault();
            if (mediaDirector == null || directorId != mediaDirector.DirectorId)
            {
                return NotFound();
            }

            _context.MediaDirectors.Remove(mediaDirector);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
