using FinalProject.Models.Entities;

namespace FinalProject.Models
{
    public class AdminViewModel
    {
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; }
        public List<BiddingLog> BiddingLogs { get; set; }
        public List<ProjectLog> ProjectLogs { get; set; }
        public List<ProjectBid> ProjectBids { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
