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
    public class MediaCategoriesController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public MediaCategoriesController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/MediaCategories
        [HttpGet]
        public ActionResult<List<MediaCategory>> GetMediaCategories()
        {

            return _context.MediaCategories.ToList();
        }

        // GET: api/MediaCategories/5
        [HttpGet("{id}")]
        public ActionResult<MediaCategory> GetMediaCategory(int mediaId)
        {
            Media media = _context.Medias.Find(mediaId)!;
            MediaCategory mediaCategory = _context.MediaCategories.Where(m => m.MediaId == media.Id).FirstOrDefault()!;
            return mediaCategory;
        }

        // PUT: api/MediaCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public ActionResult PutMediaCategory(int id, MediaCategory mediaCategory)
        //{


        //    return NoContent();
        //}

        // POST: api/MediaCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public bool PostMediaCategory(string mediaName, int categoryId)
        {
            MediaCategory mediaCategory = new MediaCategory();
            Media media = _context.Medias.Where(m => m.Name == mediaName).FirstOrDefault();
            Category category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            if (media == null || category == null)
            {
                return false;
            }
            mediaCategory.MediaId = media.Id;
            mediaCategory.CategoryId = category.Id;
            _context.MediaCategories.Add(mediaCategory);
            _context.SaveChanges();

            return true;

        }

        // DELETE: api/MediaCategories/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMediaCategory(int id)
        {
            MediaCategory mediaCategory = _context.MediaCategories.Find(id);
            if (mediaCategory == null)
            {
                return NotFound();
            }

            _context.MediaCategories.Remove(mediaCategory);
            _context.SaveChanges();

            return Content("Deleted");
        }
    } 
}
