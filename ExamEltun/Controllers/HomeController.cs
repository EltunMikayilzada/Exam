using ExamEltun.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamEltun.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbcontext _context;

        public HomeController(AppDbcontext context)
        {
           _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var index = await _context.Employees.Include(x => x.Profession).ToListAsync();
            return View(index);
        }
    }
}
