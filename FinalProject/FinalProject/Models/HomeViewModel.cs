using FinalProject.Models.Entities;

namespace FinalProject.Models
{
    //View Model that holds multiple models
    public class HomeViewModel
    {
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; }
    }
}
