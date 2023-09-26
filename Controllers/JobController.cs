using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Worktastic.Data;
using Worktastic.Models;

namespace Worktastic.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public JobController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (!user.IsCompanyAccount)
            {
                return Unauthorized();
            }

            Company company = _context.Companies.SingleOrDefault(x => x.User == user);

            if(company == null)
            {
                return NotFound();
            }

            List<Job> jobs = _context.Jobs.Where(x => x.Company.Id == company.Id).ToList();

            return View(jobs);
        }

        public async Task<IActionResult> New()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.IsCompanyAccount)
            {
                return Unauthorized();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job newJob)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.IsCompanyAccount)
            {
                return Unauthorized();
            }

            Company company = _context.Companies.SingleOrDefault(x => x.User == user);
            if(company == null)
            {
                return RedirectToAction("New", "Company");
            }

            newJob.Company = company;

            _context.Add(newJob);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var job = _context.Jobs.
                Include(j => j.Company).
                Include(j => j.Company.User).
                SingleOrDefault(j => j.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (!job.Company.User.Equals(user))
            {
                return Unauthorized();
            }

            return View(job);
        }

        [HttpPost]
        public IActionResult Update(Job job)
        {
            try
            {
                _context.Jobs.Update(job);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();

            Job job = _context.Jobs.SingleOrDefault(x => x.Id == id);

            if (job == null) return NotFound();

            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
