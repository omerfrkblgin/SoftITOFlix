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
            if(includePassive == false)
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
            

            user = _signInManager.UserManager.Users.Where(u=>u.Id== softITOFlixUser.Id).FirstOrDefault()!;

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
        public ActionResult<bool> Login(LoginModel loginModel)
        {
            Microsoft.AspNetCore.Identity.SignInResult result;
            SoftITOFlixUser user = _signInManager.UserManager.FindByNameAsync(loginModel.Username).Result;

            if(user == null)
            {
                return NotFound();
            }

            if(_context.UserPlans.Where(u => u.UserId == user.Id && u.EndDate >= DateTime.Today).Any() == false)
            {
                user.Passive= true;
                _signInManager.UserManager.UpdateAsync(user).Wait();
            }

            if(user.Passive == true)
            {
                return Content("Passive");
            }
            result = _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false).Result;

            return result.Succeeded;


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

            IdentityResult identityResult = _signInManager.UserManager.CreateAsync(softITOFlixUser).Result;

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
