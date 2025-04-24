using FinalProject.Data;
using FinalProject.Models.Entities;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    [Authorize]
    public class BiddingLogsController : Controller
    {
        private readonly AppDbContext dbContext;
        public BiddingLogsController(AppDbContext dbContext)
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
        public async Task<IActionResult> Add(BiddingLogViewModel biddingLogViewModel)
        {
            

            var biddinglog = new BiddingLog
            {
                Status = biddingLogViewModel.Status,
                OperatorId = biddingLogViewModel.OperatorId,
                Operation = biddingLogViewModel.Operation,
                Note = biddingLogViewModel.Note,
                Timestamp = biddingLogViewModel.Timestamp,
                ProjectBidId = biddingLogViewModel.ProjectBidId
                
            };

            await dbContext.BiddingLogs.AddAsync(biddinglog);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var biddinglogs = await dbContext.BiddingLogs.ToListAsync();

            return View(biddinglogs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var biddinglog = await dbContext.BiddingLogs.FindAsync(id);

            return View(biddinglog);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BiddingLog biddingLogViewModel)
        {
            var biddinglog = await dbContext.BiddingLogs.FindAsync(biddingLogViewModel.Id);

            if (biddinglog is not null)
            {
                biddinglog.Status = biddingLogViewModel.Status;
                biddinglog.OperatorId = biddingLogViewModel.OperatorId;
                biddinglog.Operation = biddingLogViewModel.Operation;
                biddinglog.Note = biddingLogViewModel.Note;
                biddinglog.Timestamp = biddingLogViewModel.Timestamp;
                biddinglog.ProjectBidId = biddingLogViewModel.ProjectBidId;


                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "BiddingLogs");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BiddingLog biddingLogViewModel)
        {
            var biddinglog = await dbContext.BiddingLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == biddingLogViewModel.Id);

            if (biddinglog is not null)
            {
                dbContext.BiddingLogs.Remove(biddingLogViewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "BiddingLogs");
        }
    }
}
