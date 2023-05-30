using Microsoft.AspNetCore.Mvc;

namespace ExamEltun.Areas.lumiaadmin.Controllers
{
    [Area("lumiaadmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
