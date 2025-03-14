using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class ProjectLog
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }

        [ForeignKey("operatorId")]
        public int OperatorId { get; set; }
        public User operatorId { get; set; }

        public string Operation { get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey("project")]
        public int ProjectId { get; set; }
        public Project project { get; set; }
    }
}
