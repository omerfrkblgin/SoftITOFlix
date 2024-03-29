using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Data;
using SoftITOFlix.Models;
using System.Security.Claims;

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
        public ActionResult<List<Episode>> GetEpisodes(int mediaId, byte seasonNumber)
        {
            
            return _context.Episodes.Where(e => e.MediaId == mediaId && e.SeasonNumber == seasonNumber).OrderBy(e => e.SeasonNumber).AsNoTracking().ToList();
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Episode> GetEpisode(long id)
        {
            Episode? episode = _context.Episodes.Find(id);

            if (episode == null)
            {
                return NotFound();
            }

            return episode;
        }

        [HttpGet("Watch")]
        [Authorize]
        public void Watch(long id)
        { 
            UserWatched userWatched = new UserWatched();
            Episode episode = _context.Episodes.Find(id)!;
            try
            {
                userWatched.UserId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                userWatched.EpisodeId = id;
                _context.UserWatches.Add(userWatched);
                episode.ViewCount++;
                _context.Episodes.Update(episode);
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                
            }
            
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public void PutEpisode(Episode episode)
        {
            _context.Episodes.Update(episode);
            _context.SaveChanges();

        }

        // POST: api/Episodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
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
            episode.Passive = true;
            _context.SaveChanges();

            return "Deleted";
        }
    }
}
