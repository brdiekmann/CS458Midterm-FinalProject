using FinalProject.Models;
using FinalProject.Models.Entities;

namespace FinalProject.Models
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public List<User>? Users { get; set; } = new List<User>();
        
		
	}
}
