using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await dbContext.Users.ToListAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await dbContext.Users.FindAsync(id);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User userViewModel)
        {
            var user = await dbContext.Users.FindAsync(userViewModel.Id);

            if (user is not null)
            {
                user.Name = userViewModel.Name; 
                user.ProfilePicture = userViewModel.ProfilePicture;
                user.Bio = userViewModel.Bio;
                user.Email = userViewModel.Email;
                user.Phone = userViewModel.Phone;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List","Users");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(User userViewModel)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>x.Id == userViewModel.Id);

            if (user is not null)
            {
                dbContext.Users.Remove(userViewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Users");
        }

    }
}
