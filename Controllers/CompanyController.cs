using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Worktastic.Data;
using Worktastic.Models;

namespace Worktastic.Controllers
{
    public class CompanyController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company, IFormFile picture) 
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

            _context.Companies.Add(company);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var company = _context.Companies.SingleOrDefault(x => x.Id == id);

            if(company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPut]
        public IActionResult Update(Company company)
        {
            try
            {
                _context.Companies.Update(company);
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
