using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace FinalProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminPortalController : Controller
    {
        private readonly AppDbContext dbContext;
        public AdminPortalController(AppDbContext dbContext)
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
            var viewModel = new AdminViewModel
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
    }
}
