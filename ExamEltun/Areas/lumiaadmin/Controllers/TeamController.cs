using ExamEltun.DAL;
using ExamEltun.Models;
using ExamEltun.Utilities.Extentions;
using ExamEltun.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExamEltun.Areas.lumiaadmin.Controllers
{
    [Area("lumiaadmin")]
    public class TeamController : Controller
    {
        private readonly AppDbcontext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbcontext context, IWebHostEnvironment env)
        {
          _context = context;
           _env = env;
        }
        public async Task<IActionResult> Index( )
        {
           List<Employee> index=await _context.Employees.Include(x=>x.Profession).ToListAsync();
            //PaginationVM<Employee> paginationVM = new PaginationVM<Employee>
            //{
            //    CurrentPage = page,
            //    TotalPage=Math.Ceiling(_context.Employees.Count() / 3)
            //    items = index
            //};
            //return View(paginationVM);
            return View(index);
        }
        public async Task<IActionResult> Create()
        {
           ViewBag.Profession=await _context.Professions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)

        {
            if (employee.Photo==null)
            {
                ModelState.AddModelError("Photo", "Photo bos ola bilmez");
                return View();
            }
             if (!employee.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image tipinden olmaliudir");
                return View();
            }
            if (!employee.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Photonun Olcusu 200 den cox olmamalidir");
                return View();
            }
            employee.Image =await employee.Photo.CreateFileAsync(_env.WebRootPath, "uploads/employeeimg");
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null||id<1)
            {
                return BadRequest();
            }
            var exsited = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (exsited==null)
            {
                return NotFound();
            }
            exsited.Image.DeleteFileAsync(_env.WebRootPath, "uploads/employeeimg");
            _context.Remove(exsited);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            var exsited = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (exsited == null)
            {
                return NotFound();
            }
            return View(exsited);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Employee employee)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            var exsited = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (exsited == null)
            {
                return NotFound();
            }
            if (employee.Photo!=null)
            {
                if (!employee.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Image tipinden olmaliudir");
                    return View();
                }
                if (!employee.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Photonun Olcusu 200 den cox olmamalidir");
                    return View();
                }
            }
            exsited.Image.DeleteFileAsync(_env.WebRootPath, "uploads/employeeimg");
            //employee.Image =await employee.Photo.CreateFileAsync(_env.WebRootPath, "uploads/employeeimg");
            exsited.Image = await employee.Photo.CreateFileAsync(_env.WebRootPath, "uploads/employeeimg");
            exsited.Name= employee.Name;
            exsited.Surname= employee.Surname;
            exsited.ProfessionId = employee.ProfessionId;
            exsited.Mesagge= employee.Mesagge;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



    }
}
