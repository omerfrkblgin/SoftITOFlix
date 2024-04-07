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
    public class CategoriesController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public CategoriesController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public ActionResult<List<Category>> GetCategory()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{categoryId}")]
        public ActionResult GetCategory(short categoryId)
        {
            List<MediaCategory> mediaCategories = _context.MediaCategories.Include(mc => mc.Media).Where(mc => mc.CategoryId == categoryId).ToList();

            if (mediaCategories == null)
            {
                return NotFound();
            }

            List<Media>? mediaList = new List<Media>();

            foreach (MediaCategory mediaCategory in mediaCategories)
            {
                mediaList.Add(mediaCategory.Media);
            }

            return Ok(mediaList);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public void PutCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public short PostCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category.Id;
        }
    }
}
