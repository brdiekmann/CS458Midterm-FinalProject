using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Authorize]
    public class ProjectBidsController : Controller
    {
        private readonly AppDbContext dbContext;
        public ProjectBidsController(AppDbContext dbContext)
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
        public async Task<IActionResult> Add(ProjectBidsViewModel projectBidsViewModel)
        {
            var projectBid = new ProjectBid
            {
                BidderId = projectBidsViewModel.BidderId,
                ProjectId = projectBidsViewModel.ProjectId,
                Status = projectBidsViewModel.Status,
                Note = projectBidsViewModel.Note,
                Proposal = projectBidsViewModel.Proposal,
                Bid = projectBidsViewModel.Bid,
                Timeline = projectBidsViewModel.Timeline,
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
                projectBid.Status = projectBidViewModel.Status;
                projectBid.Note = projectBidViewModel.Note;
                projectBid.Proposal = projectBidViewModel.Proposal;
                projectBid.Bid = projectBidViewModel.Bid;
                projectBid.Timeline = projectBidViewModel.Timeline;
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


    }
}

