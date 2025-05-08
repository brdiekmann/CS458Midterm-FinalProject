using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

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
            var bidders = dbContext.Users
                .Select(u => new { u.Id, Name = u.Name })
                .ToList();

            var projects = dbContext.Projects
                .Select(p => new {p.Id, p.Title})
                .ToList();

            ViewBag.Projects = new SelectList(projects, "Id", "Title");
            ViewBag.Bidders = new SelectList(bidders, "Id", "Name");

            return View();
        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Add(ProjectBidsViewModel projectBidsViewModel)
        { 

            if (!ModelState.IsValid)
            {
                var bidders = dbContext.Users
                .Select(u => new { u.Id, Name = u.Name })
                .ToList();

                var projects = dbContext.Projects
                    .Select(p => new { p.Id, p.Title })
                    .ToList();

                ViewBag.Projects = new SelectList(projects, "Id", "Title", projectBidsViewModel.ProjectId);
                ViewBag.Bidders = new SelectList(bidders, "Id", "Name", projectBidsViewModel.BidderId);
                return View(projectBidsViewModel);
            }

            var _project = await dbContext.Projects.FindAsync(projectBidsViewModel.ProjectId);
            if (_project == null) return NotFound();

            var _bidder = await dbContext.Users.FindAsync(projectBidsViewModel.BidderId);
            if (_bidder == null) return NotFound();


            var projectBid = new ProjectBid
            {
                project = _project,
                bidder = _bidder,
                BidderId = projectBidsViewModel.BidderId,
                ProjectId = projectBidsViewModel.ProjectId,
                BidValue = projectBidsViewModel.BidValue,
                SubmittedTime = projectBidsViewModel.SubmittedTime,
            };

            await dbContext.ProjectBids.AddAsync(projectBid);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "ProjectBids");
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
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
            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (projectBid is not null)
            {
                projectBid.ProjectId = projectBidViewModel.ProjectId;
                projectBid.BidderId = projectBidViewModel.BidderId;
                projectBid.BidValue = projectBidViewModel.BidValue;
                projectBid.SubmittedTime = projectBidViewModel.SubmittedTime;
                
                await dbContext.SaveChangesAsync();
            }

            if (isAdmin)
            {
                return RedirectToAction("List", "ProjectBids");
            }

            return RedirectToAction("MyBids", "ProjectBids");
            
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
                bidder = projectBidsViewModel.bidder,
                project = projectBidsViewModel.project,
                SubmittedTime = DateTime.Now,
            };
            await dbContext.ProjectBids.AddAsync(projectBid);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("ViewProjects", "Projects");
        }

        [HttpGet]
        public async Task<IActionResult> MyBids()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var projectBids = await dbContext.ProjectBids
                .Include(p => p.bidder)
                .Include(p => p.project)
                .Where(p => p.bidder.Id == user.Id)
                .ToListAsync();

            return View(projectBids);
        }

        public IActionResult BidMenu()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewBids()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var projectBids = await dbContext.ProjectBids
                .Include(p => p.bidder)
                .Include(p => p.project)
                .Where(p => p.bidder.Id != user.Id)
                .ToListAsync();
            if (projectBids == null)
                return NotFound();

            return View(projectBids);
        }

        [HttpGet]
        public async Task<IActionResult> BidView(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var projectBid = await dbContext.ProjectBids
                .Include(p => p.bidder)
                .Include(p => p.project)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (projectBid == null)
                return NotFound();


            return View(projectBid);
        }

    }
}

