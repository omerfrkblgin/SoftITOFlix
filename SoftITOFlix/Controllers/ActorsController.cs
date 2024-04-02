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
    public class ActorsController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public ActorsController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public ActionResult<List<Actor>> GetActors()
        {
            return _context.Actors.AsNoTracking().ToList();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public ActionResult<Actor> GetActor(int id)
        {
            Actor? actor = _context.Actors.Find(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public void PutActor(Actor actor)
        {
            _context.Actors.Update(actor);
            _context.SaveChanges();
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public int PostActor(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();

            return actor.Id;
        }
    }
}
