using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Worktastic.Data;
using Worktastic.Models;

namespace Worktastic.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CompanyController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.IsCompanyAccount)
            {
                return RedirectToAction("Index", "Home");
            }
            Company company = _context.Companies.SingleOrDefault(x => x.User == user);
            if (company == null)
            {
                return RedirectToAction("New");
            }
            return View(company);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company, IFormFile picture) 
        {
            if(picture != null)
            {
                using (var stream = new MemoryStream())
                {
                    picture.CopyTo(stream);
                    var bytes = stream.ToArray();
                    company.Image = bytes;
                }
            }

            company.User = await _userManager.GetUserAsync(User);
            
            if(company.User is null)
            {
                return BadRequest();
            }

            _context.Companies.Add(company);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var company = _context.Companies.
                Include(c => c.User).
                SingleOrDefault(x => x.Id == id);

            if(company == null)
            {
                return NotFound();
            }

            if (!company.User.Equals(user))
            {
                return Unauthorized();
            }

            return View(company);
        }

        [HttpPost]
        public IActionResult Update(Company company, IFormFile picture)
        {

            if (picture != null)
            {
                using (var stream = new MemoryStream())
                {
                    picture.CopyTo(stream);
                    var bytes = stream.ToArray();
                    company.Image = bytes;
                }
            }

            try
            {
                _context.Companies.Update(company);
                _context.SaveChanges();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();

            Company company = _context.Companies.SingleOrDefault(x => x.Id == id);

            if(company == null) return NotFound();

            _context.Companies.Remove(company);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
