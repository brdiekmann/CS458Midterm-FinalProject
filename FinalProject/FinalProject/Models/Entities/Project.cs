using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models.EntityAttributes;

namespace FinalProject.Models.Entities
{
    public class Project
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required"), MaxLength(100, ErrorMessage = "Titles must be less than 100 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required"), MaxLength(500, ErrorMessage = "Descriptions must be less than 500 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Tags are required"), MaxLength(200, ErrorMessage = "Tags must be less than 200 characters")]
        public string Tags { get; set; }
        [Required(ErrorMessage = "Deadline is required")]
        [FutureDate(ErrorMessge = "Deadline must be atleast 7 days from today!")]
        public DateTime Deadline { get; set; }
        [Required(ErrorMessage = "Technology is required")]
        [RegularExpression("Web Development|Mobile Development|Data Science|AI/ML|Cloud Computing", ErrorMessage = "Invalid technology selected.")]
        public string Technology { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("Pending|Approved|Rejected", ErrorMessage = "Invalid status selected")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Funding is required")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Funding must be a positive number")]
        public decimal Funding { get; set; }


        [ForeignKey("submitter")]
        [Required(ErrorMessage = "SubmitterId is required")]
        public string? SubmitterId { get; set; }
        public User? Submitter { get; set; }
        

    }
}
