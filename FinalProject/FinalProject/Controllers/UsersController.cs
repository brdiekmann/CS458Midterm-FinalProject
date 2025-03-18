using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext dbContext;
        public UsersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            var user = new User
            {
                Name = userViewModel.Name,
                ProfilePicture = userViewModel.ProfilePicture,
                Bio = userViewModel.Bio,
                Email = userViewModel.Email,
                Phone = userViewModel.Phone

            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

    }
}
