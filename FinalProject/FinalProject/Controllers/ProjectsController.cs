using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using System.Reflection;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FinalProject.Controllers
{
	//[Authorize]
	public class ProjectsController : Controller
	{
		private readonly AppDbContext dbContext;
        private readonly UserManager<User> userManager;
        public ProjectsController(AppDbContext dbContext, UserManager<User> userManager)
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
            var model = new ProjectViewModel
            {
                Project = new Project(),
                Users = dbContext.Users.ToList()
                
            };

            return View(model);
        }
		[HttpPost]
		public async Task<IActionResult> Add(ProjectViewModel projectViewModel)
		{
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await dbContext.Users.ToListAsync();
                return View(projectViewModel);
            }

            var submitterName = dbContext.Users
			.Where(u => u.Id.ToString() == projectViewModel.Project.SubmitterId)
			.Select(u => u.Name)
			// Retrieve only the name
			.FirstOrDefault();

			var project = new Project
			{
				Title = projectViewModel.Project.Title,
				Description = projectViewModel.Project.Description,
				Tags = projectViewModel.Project.Tags,
				Deadline = projectViewModel.Project.Deadline,
				Technology = projectViewModel.Project.Technology,
				Status = projectViewModel.Project.Status,
				Funding = projectViewModel.Project.Funding,
				SubmitterId = projectViewModel.Project.SubmitterId,
				Submitter = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == projectViewModel.Project.SubmitterId)
			};

            await dbContext.Projects.AddAsync(project);
			await dbContext.SaveChangesAsync();

			// Not sure if this is the best approach...
			var projectLog = new ProjectLog {
				Status = project.Status,
				OperatorId = project.SubmitterId ?? "",
				Operation = "Create",
				Note = project.Description,
				Timestamp = DateTime.Now,
				ProjectId = project.Id,
			};
            await dbContext.ProjectLogs.AddAsync(projectLog);
			await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Project " + project.Title + " (ID: " + project.Id + ") added successfully!";
            return RedirectToAction("List");
		}
		[HttpGet]
		public async Task<IActionResult> List()
		{
			var projects = await dbContext.Projects
				.Include(p => p.Submitter)
				.ToListAsync();

			return View(projects);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
            var project = await dbContext.Projects
            .Include(p => p.Submitter)
			.FirstOrDefaultAsync(p => p.Id == id);

            ViewBag.Users = dbContext.Users.ToList();

            if (project == null)
                return NotFound();

            return View(project);

		}
		[HttpPost]
		public async Task<IActionResult> Edit(Project projectViewModel)
		{
            if (!ModelState.IsValid) return View(projectViewModel);
            var project = await dbContext.Projects.FindAsync(projectViewModel.Id);

			if (project is not null)
			{
				project.Title = projectViewModel.Title;
				project.Description = projectViewModel.Description;
				project.Tags = projectViewModel.Tags;
				project.Deadline = projectViewModel.Deadline;
				project.Technology = projectViewModel.Technology;
				project.Status = projectViewModel.Status;
				project.Funding = projectViewModel.Funding;
				project.SubmitterId = projectViewModel.SubmitterId;
				

				//await dbContext.SaveChangesAsync();

                // Not sure if this is the best approach...
                var projectLog = new ProjectLog {
                    Status = project.Status,
                    OperatorId = project.SubmitterId ?? "",
                    Operation = "Edit",
                    Note = project.Description,
                    Timestamp = DateTime.Now,
                    ProjectId = project.Id,
                };
                await dbContext.ProjectLogs.AddAsync(projectLog);
                await dbContext.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Project " + project.Title + " (ID: " + project.Id + ") added successfully!";

            return RedirectToAction("List", "Projects");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Project projectViewModel, ProjectLog projectLogViewModel) {
			var project = await dbContext.Projects
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == projectViewModel.Id);
            var projectbids = await dbContext.ProjectBids
                .AsNoTracking()
                .Where(u => u.ProjectId == projectViewModel.Id)
                .ToListAsync();
            if (projectbids == null) return NotFound();

            var user = await userManager.GetUserAsync(User);

            if (user == null) return NotFound();
            if (project == null) return NotFound();


            var projectLogs = await dbContext.ProjectLogs
				.AsNoTracking()
				.Where(w => w.ProjectId == projectViewModel.Id)
				.ToListAsync();
			if (projectLogs == null) return NotFound();

            if (project is not null) {
				dbContext.Projects.Remove(project);
				dbContext.ProjectBids.RemoveRange(projectbids);
				dbContext.ProjectLogs.RemoveRange(projectLogs);
                await dbContext.SaveChangesAsync();
            }

            if (userManager.GetRolesAsync(user).ToString() != "Admin")
            {
                return RedirectToAction("MyProjects", "Projects");
            }
            else
            {
                return RedirectToAction("List", "Projects");
            }
		}

		public IActionResult ProjectMenu()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> ViewProjects()
		{
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var projects = await dbContext.Projects
                .Include(p => p.Submitter)
                .Where(p => p.Submitter.Id != user.Id)
                .ToListAsync();
            if (projects == null)
                return NotFound();

            return View(projects);
        }

		[HttpGet]
		public async Task<IActionResult> ProjectView(int id)
		{
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var project = await dbContext.Projects
				.Include(p => p.Submitter)
                .Where(p => p.Submitter.Id != user.Id)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();
            

            return View(project);
        }
		[HttpGet]
		public async Task<IActionResult> MyProjects()
		{
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var projects = await dbContext.Projects
                .Include(p => p.Submitter)
				.Where(p => p.Submitter.Id==user.Id)
                .ToListAsync();

			return View(projects);
        }
		[HttpGet]
		public async Task<IActionResult> EditMyProject(int id)
		{
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var project = await dbContext.Projects
            .Include(p => p.Submitter)
            .Where(u => u.Submitter.Id == user.Id)
            .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return NotFound();

            return View(project);
        }
		[HttpPost]
        public async Task<IActionResult> EditMyProject(Project projectViewModel)
		{
            if (!ModelState.IsValid) return View(projectViewModel);
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var project = await dbContext.Projects.FindAsync(projectViewModel.Id);

            if (project is not null)
            {
                project.Title = projectViewModel.Title;
                project.Description = projectViewModel.Description;
                project.Tags = projectViewModel.Tags;
                project.Deadline = projectViewModel.Deadline;
                project.Technology = projectViewModel.Technology;
                project.Status = projectViewModel.Status;
                project.Funding = projectViewModel.Funding;
                project.SubmitterId = user.Id;
                project.Submitter = user;


                await dbContext.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = project.Title +" edited successfully!";

            return RedirectToAction("MyProjects", "Projects");
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var project = new Project
            {
                SubmitterId = user.Id,
                Submitter = user
            };
            var model = new ProjectViewModel
            {
                Project = project
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNew(ProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            { 
                return View(projectViewModel);
            }

            var project = new Project
            {
                Title = projectViewModel.Project.Title,
                Description = projectViewModel.Project.Description,
                Tags = projectViewModel.Project.Tags,
                Deadline = projectViewModel.Project.Deadline,
                Technology = projectViewModel.Project.Technology,
                Status = projectViewModel.Project.Status,
                Funding = projectViewModel.Project.Funding,
                SubmitterId = projectViewModel.Project.SubmitterId,
                Submitter = projectViewModel.Project.Submitter
            };

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = project.Title + " added successfully!";
            return RedirectToAction("MyProjects");
        }


    }
}

