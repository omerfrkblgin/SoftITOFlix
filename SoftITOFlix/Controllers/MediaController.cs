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
    public class MediaController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public MediaController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Media
        [HttpGet]
        public ActionResult<List<Media>> GetMedias()
        {
            return _context.Medias.AsNoTracking().ToList();
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
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
        public void PutMedia(Media media)
        {
            _context.Medias.Update(media);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
        }

        // POST: api/Media
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public int PostMedia(Media media)
        {
            _context.Medias.Add(media);
            _context.SaveChanges();

            return media.Id;
        }

        // DELETE: api/Media/5
        [HttpDelete("{id}")]
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
