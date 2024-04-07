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
    public class RestrictionsController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public RestrictionsController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Restrictons
        [HttpGet]
        public ActionResult<List<Restricton>> GetRestrictons()
        {
            return _context.Restrictons.ToList();
        }

        // GET: api/Restrictons/5
        [HttpGet("{id}")]
        public ActionResult<Restricton> GetRestricton(byte id)
        {
            Restricton? restricton = _context.Restrictons.Find(id);

            if (restricton == null)
            {
                return NotFound();
            }

            return restricton;
        }

        // PUT: api/Restrictons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public void PutRestricton(Restricton restricton)
        {
            _context.Restrictons.Update(restricton);
            _context.SaveChanges();
        }

        // POST: api/Restrictons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public byte PostRestricton(Restricton restricton)
        {
            _context.Restrictons.Add(restricton);
            _context.SaveChanges();
            return restricton.Id;
        }


    }
}
