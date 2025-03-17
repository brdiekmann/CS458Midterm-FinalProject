using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add()
        {

        }
    }
}
