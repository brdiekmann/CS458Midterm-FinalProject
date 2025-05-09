using System.Diagnostics;
using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext dbContext;
        public HomeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = dbContext.Users.ToList();
            var projects = dbContext.Projects.ToList();
            var biddinglogs = dbContext.BiddingLogs.ToList();
            var projectlogs = dbContext.ProjectLogs.ToList();
            var attachments = dbContext.Attachments.ToList();
            var projectbids = dbContext.ProjectBids.ToList();
            var viewModel = new HomeViewModel
            {
                Users = users,
                Projects = projects,
                BiddingLogs = biddinglogs,
                ProjectLogs = projectlogs,
                Attachments = attachments,
                ProjectBids = projectbids
            };
            return View(viewModel);
}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
