using FinalProject.Data;
using FinalProject.Models.Entities;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Evaluation;

namespace FinalProject.Controllers
{
    [Authorize]
    public class AttachmentsController : Controller
    {
        private readonly AppDbContext dbContext;
        public AttachmentsController(AppDbContext dbContext)
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
        public async Task<IActionResult> Add(AttachmentViewModel attachmentViewModel)
        {
            var attachment = new Attachment
            {
                Type = attachmentViewModel.Attachment.Type,
                Name = attachmentViewModel.Attachment.Name,
                Location = attachmentViewModel.Attachment.Location,
                LastModifiedTimestamp = attachmentViewModel.Attachment.LastModifiedTimestamp,
                ProjectId = attachmentViewModel.Attachment.ProjectId,
            };

            await dbContext.Attachments.AddAsync(attachment);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var attachmemts = await dbContext.Attachments
                .ToListAsync();

            return View(attachmemts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attachment = await dbContext.Attachments.FindAsync(id);

            return View(attachment);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Attachment attachmentViewModel)
        {
            var attachment = await dbContext.Attachments.FindAsync(attachmentViewModel.Id);

            if (attachment is not null)
            {
                attachment.Type = attachmentViewModel.Type;
                attachment.Name = attachmentViewModel.Name;
                attachment.Location = attachmentViewModel.Location;
                attachment.LastModifiedTimestamp = attachmentViewModel.LastModifiedTimestamp;
                attachment.ProjectId = attachmentViewModel.ProjectId;


                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Attachments");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Attachment attachmentViewModel)
        {
            var attachment = await dbContext.Attachments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == attachmentViewModel.Id);

            if (attachment is not null)
            {
                dbContext.Attachments.Remove(attachmentViewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Attachments");
        }
        [HttpGet]
        public async Task<IActionResult> ViewProjectAttachments(int id)
        {
            var attachments = await dbContext.Attachments
            .Where(p => p.ProjectId == id)
            .ToListAsync();
            var selectedProject = await dbContext.Projects
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (attachments == null) return NotFound();
            var attachment = new Attachment
            {
                ProjectId = id,
                project = selectedProject,
                LastModifiedTimestamp = DateTime.UtcNow,
            };
            var model = new AttachmentViewModel
            {
                Attachment = attachment,
                AttachmentList = attachments
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddAttachment(int id)
        {
            var selectedProject = await dbContext.Projects
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            var attachment = new Attachment
            {
                ProjectId = id,
                project = selectedProject,
                LastModifiedTimestamp = DateTime.UtcNow,
            };
            var attachmentViewModel = new AttachmentViewModel
            {
                Attachment = attachment
            };

            return View(attachmentViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddAttachment(AttachmentViewModel attachmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(attachmentViewModel);
            }
            var attachment = new Attachment
            {
                Type = attachmentViewModel.Attachment.Type,
                Name = attachmentViewModel.Attachment.Name,
                Location = attachmentViewModel.Attachment.Location,
                LastModifiedTimestamp = attachmentViewModel.Attachment.LastModifiedTimestamp,
                project = attachmentViewModel.Attachment.project,
                ProjectId = attachmentViewModel.Attachment.ProjectId
            };

            await dbContext.Attachments.AddAsync(attachment);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("ViewProjectAttachments", new { id = attachment.ProjectId });
        }

        [HttpGet]
        public async Task<IActionResult> EditAttachment(int id)
        {

            var attachment = await dbContext.Attachments
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync(p => p.Id == id);

            if (attachment == null) return NotFound();

            return View(attachment);

        }
        [HttpPost]
        public async Task<IActionResult> EditAttachment(Attachment attachmentViewModel)
        {
            if (!ModelState.IsValid) return View(attachmentViewModel);
            var attachment = await dbContext.Attachments.FindAsync(attachmentViewModel.Id);

            if (attachment is not null)
            {
                attachment.Type = attachmentViewModel.Type;
                attachment.Name = attachmentViewModel.Name;
                attachment.Location = attachmentViewModel.Location;
                attachment.LastModifiedTimestamp = attachmentViewModel.LastModifiedTimestamp;
                attachment.project = attachmentViewModel.project;
                attachment.ProjectId = attachmentViewModel.ProjectId;

                await dbContext.SaveChangesAsync();
                return RedirectToAction("ViewProjectAttachments", new { id = attachment.ProjectId });
            }
            return View(attachment);

        }
    }
}
