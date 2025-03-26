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
            var model = new ProjectLogViewModel
            {
                ProjectLog = new ProjectLog(),
                Users = dbContext.Users.ToList(),
                Projects = dbContext.Projects.ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProjectLogViewModel projectLogModel)
        {
            if (!ModelState.IsValid) { 
                projectLogModel.Users = dbContext.Users.ToList();
                projectLogModel.Projects = dbContext.Projects.ToList();
                return View(projectLogModel);
            }

            var projectLog = new ProjectLog
            {
                Status = projectLogModel.ProjectLog.Status,
                OperatorId = projectLogModel.ProjectLog.OperatorId,
                Operation = projectLogModel.ProjectLog.Operation,
                Note = projectLogModel.ProjectLog.Note,
                Timestamp = projectLogModel.ProjectLog.Timestamp,
                ProjectId = projectLogModel.ProjectLog.ProjectId

            };

            await dbContext.ProjectLogs.AddAsync(projectLog);
            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Project Log (ID:" + projectLog.Id + ") added successfully.";

            return RedirectToAction("List");
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
            var projectLog = await dbContext.ProjectLogs
                                    .Include(pl => pl.operatorId)
                                    .Include(pl => pl.project)
                                    .FirstOrDefaultAsync(pl => pl.Id == id);

            if (projectLog == null) return NotFound();

            ViewBag.Users = dbContext.Users.ToList();
            ViewBag.Projects = dbContext.Projects.ToList();

            return View(projectLog);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectLog projectLogModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await dbContext.Users.ToListAsync();
                ViewBag.Projects = await dbContext.Projects.ToListAsync();
                return View(projectLogModel);
            }

            var projectlog = await dbContext.ProjectLogs.FindAsync(projectLogModel.Id);
            if (projectlog == null) return NotFound();

            projectlog.Status = projectLogModel.Status;
            projectlog.OperatorId = projectLogModel.OperatorId;
            projectlog.Operation = projectLogModel.Operation;
            projectlog.Note = projectLogModel.Note;
            projectlog.Timestamp = projectLogModel.Timestamp;
            projectlog.ProjectId = projectLogModel.ProjectId;

            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Project Log (ID:" + projectlog.Id + ") edited successfully.";

            return RedirectToAction("List");
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

            return RedirectToAction("List");
        }
    }
}
