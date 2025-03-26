using FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models.Entities;

namespace FinalProject.Models
{
    public class ProjectLogViewModel
    {
        public ProjectLog ProjectLog { get; set; }
        public List<User>? Users {  get; set; } = new List<User>();
        public List<Project>? Projects { get; set; } = new List<Project>();

    }
}
