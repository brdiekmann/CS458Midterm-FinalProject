using FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //DbSets for the Models
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectBid> ProjectBids { get; set; }
        public DbSet<BiddingLog> BiddingLogs { get; set; }
        public DbSet<ProjectLog> ProjectLogs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fill Tables
            base.OnModelCreating(modelBuilder);
            /* Fill this in later basic model bellow of filling data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "John",
                    ProfilePicture = "John.png",
                    Bio = "John Bio",
                    Email = "johnmail@mail",
                    Phone = "(912) 745-8390"
                },
            */
        }
    }
}
