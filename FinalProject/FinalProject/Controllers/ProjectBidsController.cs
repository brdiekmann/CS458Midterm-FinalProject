using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Controllers
{
    [Authorize]
    public class ProjectBidsController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<User> userManager;
        public ProjectBidsController(AppDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;

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
        public async Task<IActionResult> Add(ProjectBidsViewModel projectBidsViewModel)
        {
            var projectBid = new ProjectBid
            {
                BidderId = projectBidsViewModel.BidderId,
                ProjectId = projectBidsViewModel.ProjectId,
                BidValue = projectBidsViewModel.BidValue,
                SubmittedTime = projectBidsViewModel.SubmittedTime,
            };

            await dbContext.ProjectBids.AddAsync(projectBid);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var projectBid = await dbContext.ProjectBids
                .Include(p => p.bidder)
                .Include(p => p.project)
                .ToListAsync();

            return View(projectBid);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var projectBid = await dbContext.ProjectBids.FindAsync(id);

            return View(projectBid);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectBid projectBidViewModel)
        {
            var projectBid = await dbContext.ProjectBids.FindAsync(projectBidViewModel.Id);

            if (projectBid is not null)
            {
                projectBid.ProjectId = projectBidViewModel.ProjectId;
                projectBid.BidderId = projectBidViewModel.BidderId;
                projectBid.BidValue = projectBidViewModel.BidValue;
                projectBid.SubmittedTime = projectBidViewModel.SubmittedTime;
                
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "ProjectBids");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProjectBid projectBidViewModel)
        {
            var projectBid = await dbContext.ProjectBids
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == projectBidViewModel.Id);

            if (projectBid is not null)
            {
                dbContext.ProjectBids.Remove(projectBidViewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "ProjectBids");
        }
        [HttpGet]
        public async Task<IActionResult> Bid(int Id)
        {
            var _project = await dbContext.Projects.FindAsync(Id);
            if (_project == null) return NotFound();

            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var viewModel = new ProjectBidsViewModel
            {
                ProjectId = _project.Id,
                project = _project,
                BidderId = user.Id,
                bidder = user
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Bid(ProjectBidsViewModel projectBidsViewModel)
        {
            var projectBid = new ProjectBid
            {
                ProjectId = projectBidsViewModel.ProjectId,
                BidderId = projectBidsViewModel.BidderId,
                BidValue = projectBidsViewModel.BidValue,
                SubmittedTime = DateTime.Now,
            };
            await dbContext.ProjectBids.AddAsync(projectBid);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("ViewProjects", "Projects");
        }

    }
}

