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

namespace FinalProject.Controllers
{
	//[Authorize]
	public class ProjectsController : Controller
	{
		private readonly AppDbContext dbContext;
		public ProjectsController(AppDbContext dbContext)
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
				

				await dbContext.SaveChangesAsync();
			}

            TempData["SuccessMessage"] = "Project " + project.Title + " (ID: " + project.Id + ") added successfully!";

            return RedirectToAction("List", "Projects");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Project projectViewModel)
		{
			var project = await dbContext.Projects
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == projectViewModel.Id);

			if (project is not null)
			{
				dbContext.Projects.Remove(project);
				await dbContext.SaveChangesAsync();
			}

			return RedirectToAction("List", "Projects");
		}

		[HttpGet]
		public async Task<IActionResult> ViewProjects()
		{
            var projects = await dbContext.Projects
                .Include(p => p.Submitter)
                .ToListAsync();

            return View(projects);
        }

		[HttpGet]
		public async Task<IActionResult> ProjectView(int id)
		{
            var project = await dbContext.Projects
				.Include(p => p.Submitter)
				.FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

	}
}

