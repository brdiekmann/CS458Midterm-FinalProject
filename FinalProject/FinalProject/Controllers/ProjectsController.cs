using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
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
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Add(ProjectViewModel projectViewModel)
		{

			var project = new Project
			{
				Title = projectViewModel.Title,
				Description = projectViewModel.Description,
				Tags = projectViewModel.Tags,
				Deadline = projectViewModel.Deadline,
				Technology = projectViewModel.Technology,
				Status = projectViewModel.Status,
				Funding = projectViewModel.Funding,
				SubmitterId = projectViewModel.SubmitterId,
				submitter = projectViewModel.submitter
			};

			await dbContext.Projects.AddAsync(project);
			await dbContext.SaveChangesAsync();
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> List()
		{
			var projects = await dbContext.Projects.ToListAsync();

			return View(projects);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var project = await dbContext.Projects.FindAsync(id);

			return View(project);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Project projectViewModel)
		{
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
				project.submitter = projectViewModel.submitter;

				await dbContext.SaveChangesAsync();
			}

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
				dbContext.Projects.Remove(projectViewModel);
				await dbContext.SaveChangesAsync();
			}

			return RedirectToAction("List", "Projects");
		}

	}
}

