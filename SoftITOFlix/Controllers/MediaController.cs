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
    public class MediaController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public MediaController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Media
        [HttpGet]
        [Authorize]
        public ActionResult<List<Media>> GetMedias(bool includePassive = true)
        {
            IQueryable<Media> media = _context.Medias;
            if (includePassive == false)
            {
                media = media.Where(m => m.Passive == false);
            }
            return media.AsNoTracking().ToList();
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Media> GetMedia(int id)
        {
            Media? media = _context.Medias.Find(id);
            if (media == null)
            {
                return NotFound();
            }

            return media;
        }

        // PUT: api/Media/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public void PutMedia(Media media)
        {
            _context.Medias.Update(media);
            _context.SaveChanges();
        }

        // POST: api/Media
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public int PostMedia(Media media)
        {
            _context.Medias.Add(media);
            _context.SaveChanges();

            return media.Id;
        }

        // DELETE: api/Media/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public string DeleteMedia(int id)
        {
            Media? media = _context.Medias.Find(id);
            if (media == null)
            {
                return "Null";
            }

            media.Passive = false;
            _context.SaveChanges();

            return "Deleted";
        }
    }
}
