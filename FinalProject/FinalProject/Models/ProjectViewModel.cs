using FinalProject.Models;
using FinalProject.Models.Entities;

namespace FinalProject.Models
{
    public class ProjectViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags {  get; set; }
        public DateTime Deadline { get; set; }
        public string Technology { get; set; }
		public string Status { get; set; }
        public decimal Funding { get; set; }
		public int SubmitterId { get; set; }
		public User submitter { get; set; }
	}
}
