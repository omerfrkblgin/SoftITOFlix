using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class EpisodesController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public EpisodesController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Episodes
        [HttpGet]
        [Authorize]
        public ActionResult<List<Episode>> GetEpisodes(bool includePassive = true)
        {
            IQueryable<Episode> episodes = _context.Episodes;
            if (includePassive == false)
            {
                episodes = episodes.Where(e => e.Passive == false);
            }
            return episodes.AsNoTracking().ToList();
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Episode> GetEpisode(long id)
        {
            Episode? episode = null;

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
            {
                return Unauthorized();
            }

            episode = _context.Episodes.Find(id);

            if (episode == null)
            {
                return NotFound();
            }

            return episode;
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutEpisode(Episode episode)
        {
            _context.Episodes.Update(episode);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
        }

        // POST: api/Episodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public long PostEpisode(Episode episode)
        {

            _context.Episodes.Add(episode);
            _context.SaveChanges();

            return episode.Id;
        }

        // DELETE: api/Episodes/5
        [HttpDelete("{id}")]
        public string DeleteEpisode(long id)
        {

            Episode? episode = _context.Episodes.Find(id);
            if (episode == null)
            {
                return "null";
            }
            episode.Passive = false;
            _context.SaveChanges();

            return "Deleted";
        }
    }
}
