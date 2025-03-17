using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
