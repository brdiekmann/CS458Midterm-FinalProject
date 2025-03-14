using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //DbSets for the Models
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectBid> projectBids { get; set; }
        public DbSet<BiddingLog> biddingLog { get; set; }
        public DbSet<ProjectLog> projectLogs { get; set; }
        public DbSet<Attachment> attachments { get; set; }

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
