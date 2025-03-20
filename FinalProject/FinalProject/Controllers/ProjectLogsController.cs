using FinalProject.Data;
using FinalProject.Models.Entities;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Azure;
using Microsoft.EntityFrameworkCore;
using AspNetCoreGeneratedDocument;

namespace FinalProject.Controllers
{
    public class ProjectLogsController : Controller
    {
        private readonly AppDbContext dbContext;
        public ProjectLogsController(AppDbContext dbContext)
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
        public async Task<IActionResult> Add(ProjectLogViewModel projectLogModel)
        {
            if (!ModelState.IsValid) return View(projectLogModel);

            var projectLog = new ProjectLog
            {
                Status = projectLogModel.Status,
                OperatorId = projectLogModel.OperatorId,
                Operation = projectLogModel.Operation,
                Note = projectLogModel.Note,
                Timestamp = projectLogModel.Timestamp,
                ProjectId = projectLogModel.ProjectId

            };

            await dbContext.ProjectLogs.AddAsync(projectLog);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "ProjectLogs");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var projectLogs = await dbContext.ProjectLogs.ToListAsync();

            return View(projectLogs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var projectLog = await dbContext.ProjectLogs.FindAsync(id);

            return View(projectLog);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectLog projectLogModel)
        {
            var projectlog = await dbContext.ProjectLogs.FindAsync(projectLogModel.Id);

            if (projectlog is not null)
            {
                projectlog.Status = projectLogModel.Status;
                projectlog.OperatorId = projectLogModel.OperatorId;
                projectlog.Operation = projectLogModel.Operation;
                projectlog.Note = projectLogModel.Note;
                projectlog.Timestamp = projectLogModel.Timestamp;
                projectlog.ProjectId = projectLogModel.ProjectId;

                await dbContext.SaveChangesAsync();
            }
            

            return RedirectToAction("List", "ProjectLogs");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProjectLog projectLogModel)
        {
            var projectlog = await dbContext.ProjectLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == projectLogModel.Id);

            if (projectlog is not null)
            {
                dbContext.ProjectLogs.Remove(projectLogModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "ProjectLogs");
        }
    }
}
