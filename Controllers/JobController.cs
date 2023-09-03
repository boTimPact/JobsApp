using Microsoft.AspNetCore.Mvc;
using Worktastic.Data;
using Worktastic.Models;

namespace Worktastic.Controllers
{
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext context)
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
        public IActionResult Create(Job newJob)
        {
            Company company = new Company() 
            {
                Name = "Test"
            };

            newJob.Company = company;

            _context.Add(newJob);


            return RedirectToAction("Index");
        }
    }
}
