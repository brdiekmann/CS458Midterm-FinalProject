using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Db Sets for the Models
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectBid> ProjectBids { get; set; }
        public DbSet<BiddingLog> BiddingLogs { get; set; }
        public DbSet<ProjectLog> ProjectLogs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<ProjectBid>()
                .HasOne(pb => pb.project)
                .WithMany()
                .HasForeignKey(pb => pb.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectBid>()
                .HasOne(pb => pb.bidder) 
                .WithMany()
                .HasForeignKey(pb => pb.BidderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectLog>()
            .HasOne(pl => pl.project)
            .WithMany()
            .HasForeignKey(pl => pl.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

        }
    }
}
