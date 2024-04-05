using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UserPlansController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;
        private readonly UserManager<SoftITOFlixUser> _userManager;

        public UserPlansController(SoftITOFlixContext context, UserManager<SoftITOFlixUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/UserPlans
        [HttpGet]
        public ActionResult<List<UserPlan>> GetUserPlans()
        {
            return _context.UserPlans.ToList();
        }

        // GET: api/UserPlans/5
        [HttpGet("{id}")]
        public ActionResult<UserPlan> GetUserPlan(long id)
        {
            var userPlan = _context.UserPlans.Find(id);

            if (userPlan == null)
            {
                return NotFound();
            }

            return userPlan;
        }

        // PUT: api/UserPlans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutUserPlan(long id, UserPlan userPlan)
        {
            if (id != userPlan.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userPlan).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }

        // POST: api/UserPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostUserPlan(string email, short planId)
        {
            Plan? plan = _context.Plans.Find(planId);
            SoftITOFlixUser? user = _userManager.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                UserPlan userPlan = new UserPlan();
                userPlan.UserId = user.Id;
                userPlan.PlanId = plan.Id;
                userPlan.StartDate = DateTime.Today;
                userPlan.EndDate = userPlan.StartDate.AddMonths(1);
                user.Passive = false;
                _context.UserPlans.Add(userPlan);
                _context.SaveChanges();
            }
            
        }

        // DELETE: api/UserPlans/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUserPlan(long id)
        {
            UserPlan? userPlan = _context.UserPlans.Find(id);
            if (userPlan == null)
            {
                return NotFound();
            }

            _context.UserPlans.Remove(userPlan);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
