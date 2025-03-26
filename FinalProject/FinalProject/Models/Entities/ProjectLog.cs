using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models.Entities;

namespace FinalProject.Models.Entities
{
    public class ProjectLog
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression(@"^(Active|Inactive|On Hold)$", ErrorMessage = "Status must be one of the following values: Active, Inactive, or On Hold.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Operator is required.")]
        [ForeignKey("operatorId")]
        public int OperatorId { get; set; }

        public User? operatorId { get; set; }

        [Required(ErrorMessage = "Operation is required.")]
        [RegularExpression(@"^(Regular|Unique)$", ErrorMessage = "Operation must be one of the following values: Regular or Unique.")]
        public string Operation { get; set; }

        [Required(ErrorMessage = "Note is required.")]
        [StringLength(48, ErrorMessage = "Note cannot exceed 48 characters.")]
        public string Note { get; set; }


        [Required(ErrorMessage = "Timestamp is required.")]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        [ForeignKey("project")]
        public int ProjectId { get; set; }
        public Project? project { get; set; }
    }
}
