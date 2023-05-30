using ExamEltun.DAL;
using ExamEltun.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamEltun.Areas.lumiaadmin.Controllers
{
    [Area("lumiaadmin")]
    public class ServiceController : Controller
    {
        private readonly AppDbcontext _context;

        public ServiceController(AppDbcontext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var index=_context.Settings.ToList();
            return View(index);
        }
        public async Task<IActionResult> Update(int?id )
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            var exsited = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (exsited == null)
            {
                return NotFound();
            }
            return View(exsited);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Settings newvalue)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            var exsited = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (exsited == null)
            {
                return NotFound();
            }
            exsited.Value = newvalue.Value;
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

    }
}
