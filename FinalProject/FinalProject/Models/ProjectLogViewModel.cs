using FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class ProjectLogViewModel
    {
        public string Status { get; set; }
        public int OperatorId { get; set; }
        public string Operation { get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; }
        public int ProjectId { get; set; }
    }
}
