using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Project
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime Deadline { get; set; }
        public string Technology { get; set; }
        public string Status { get; set; }
        public decimal Funding { get; set; }

        [ForeignKey("submitter")]
        public int SubmitterId { get; set; }
        public User submitter { get; set; }
    }
}
