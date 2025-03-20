using FinalProject.Data;
using FinalProject.Models.Entities;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
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
                Type = attachmentViewModel.Type,
                Name = attachmentViewModel.Name,
                Location = attachmentViewModel.Location,
                LastModifiedTimestamp = attachmentViewModel.LastModifiedTimestamp,
                ProjectId = attachmentViewModel.ProjectId,
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
    }
}
