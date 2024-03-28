﻿using System;
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
    public class DirectorsController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public DirectorsController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Directors
        [HttpGet]
        public ActionResult<List<Director>> GetDirectors()
        {
            return _context.Directors.AsNoTracking().ToList();
        }

        // GET: api/Directors/5
        [HttpGet("{id}")]
        public ActionResult<Director> GetDirector(int id)
        {
            Director? director = _context.Directors.Find(id);

            if (director == null)
            {
                return NotFound();
            }

            return director;
        }

        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutDirector(Director director)
        {
            _context.Directors.Update(director);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {   
            }
        }

        // POST: api/Directors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public int PostDirector(Director director)
        {
            _context.Directors.Add(director);
            _context.SaveChanges();

            return director.Id;
        }
    }
}