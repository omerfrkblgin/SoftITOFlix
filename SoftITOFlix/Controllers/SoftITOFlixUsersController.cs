using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Data;
using SoftITOFlix.Models;

namespace SoftITOFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftITOFlixUsersController : ControllerBase
    {
        private readonly SignInManager<SoftITOFlixUser> _signInManager;
        private readonly SoftITOFlixContext _context;


        public SoftITOFlixUsersController(SignInManager<SoftITOFlixUser> signInManager, SoftITOFlixContext context)
        {
            _signInManager = signInManager;
            _context = context;

        }
        public struct LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // GET: api/SoftITOFlixUsers
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<SoftITOFlixUser>> GetUsers(bool includePassive = true)
        {
            IQueryable<SoftITOFlixUser> users = _signInManager.UserManager.Users;
            if (includePassive == false)
            {
                users = users.Where(u => u.Passive == false);
            }

            return users.AsNoTracking().ToList();
        }

        // GET: api/SoftITOFlixUsers/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<SoftITOFlixUser> GetSoftITOFlixUser(long id)
        {
            SoftITOFlixUser? softITOFlixUser = null;
            if (User.IsInRole("Admin") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }
            softITOFlixUser = _signInManager.UserManager.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefault();

            if (softITOFlixUser == null)
            {
                return NotFound();
            }

            return softITOFlixUser;
        }

        // PUT: api/SoftITOFlixUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult PutSoftITOFlixUser(SoftITOFlixUser softITOFlixUser)
        {
            SoftITOFlixUser? user = null;

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != softITOFlixUser.Id.ToString())
            {
                return Unauthorized();
            }


            user = _signInManager.UserManager.Users.Where(u => u.Id == softITOFlixUser.Id).FirstOrDefault()!;

            if (user == null)
            {
                return NotFound();
            }
            user.PhoneNumber = softITOFlixUser.PhoneNumber;
            user.UserName = softITOFlixUser.UserName;
            user.BirthDate = softITOFlixUser.BirthDate;
            user.Email = softITOFlixUser.Email;
            user.Name = softITOFlixUser.Name;

            _signInManager.UserManager.UpdateAsync(user).Wait();

            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult<List<Media>> Login(LoginModel loginModel)
        {
            Microsoft.AspNetCore.Identity.SignInResult result;
            SoftITOFlixUser user = _signInManager.UserManager.FindByNameAsync(loginModel.Username).Result;
            List<Media> medias = new List<Media>();
            IQueryable<Media> mediaQuery;
            IQueryable<int> userWatches;
            IGrouping<short, MediaCategory>? mediaCategories;

            if (user == null)
            {
                return NotFound();
            }

            result = _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false).Result;

            if (User.IsInRole("Admin") == false)
            {
                if (User.IsInRole("ContentAdmin") == false)
                {
                    if (_context.UserPlans.Where(u => u.UserId == user.Id && u.EndDate >= DateTime.Today).Any() == false)
                    {
                        user.Passive = true;
                        _signInManager.UserManager.UpdateAsync(user).Wait();
                    }
                }  
            }

            if (user.Passive == true)
            {
                return Content("Passive");
            }


            if (result.Succeeded == true)
            {
                mediaCategories = _context.UserFavorites.Where(u => u.UserId == user.Id).
                   Include(u => u.Media!).
                   Include(u => u.Media!.MediaCategories).
                   ToList().
                   SelectMany(u => u.Media!.MediaCategories!).
                   GroupBy(m => m.CategoryId).
                   OrderByDescending(m => m.Count()).
                   FirstOrDefault();
                if (mediaCategories != null)
                {
                    userWatches = _context.UserWatches.Where(u => u.UserId == user.Id).Include(u => u.Episode).Select(u => u.Episode!.MediaId).Distinct();
                    mediaQuery = _context.Medias.Include(m => m.MediaCategories).Where(m => m.MediaCategories!.Any(mc => mc.CategoryId == mediaCategories.Key) && !userWatches.Contains(m.Id));
                    if (user.Restriction != null)
                    {
                        //old code:
                        //mediaQuery = mediaQuery.Include(m => m.MediaRestrictions.Where(r => r.RestrictionId != user.Restriction));
                        mediaQuery = mediaQuery
                        .Include(m => m.MediaRestrictions)
                        .Where(m => !m.MediaRestrictions.Any(r => r.RestrictionId == user.Restriction));
                    }
                    medias = mediaQuery.ToList();
                }

            }
            return medias;
        }

        [HttpPost("Logout")]
        public void Logout()
        {
            _signInManager.SignOutAsync().Wait();
        }

        [HttpPost("AssignRole")]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignRole(string email)
        {
            SoftITOFlixUser? user = _signInManager.UserManager.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            _signInManager.UserManager.AddToRoleAsync(user, "ContentAdmin").Wait();
            return Ok();
        }

        [HttpPost("RemoveRole")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveRole(string email)
        {
            SoftITOFlixUser? user = _signInManager.UserManager.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            _signInManager.UserManager.RemoveFromRoleAsync(user, "ContentAdmin").Wait();
            return Ok();
        }

        // POST: api/SoftITOFlixUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<string> PostSoftITOFlixUser(SoftITOFlixUser softITOFlixUser)
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return BadRequest();
            }

            IdentityResult identityResult = _signInManager.UserManager.CreateAsync(softITOFlixUser, softITOFlixUser.Password).Result;

            if (identityResult != IdentityResult.Success)
            {
                return identityResult.Errors.FirstOrDefault()!.Description;
            }
            return Ok(softITOFlixUser.Id);
        }

        // DELETE: api/SoftITOFlixUsers/5
        [HttpDelete("{id}")]
        public ActionResult DeleteSoftITOFlixUser(long id)
        {
            SoftITOFlixUser? user = null;
            if (User.IsInRole("CustomerRepresentative") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }

            user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault()!;


            if (user == null)
            {
                return NotFound();
            }

            user.Passive = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}