using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class ProjectBid
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }

        [ForeignKey("project")]
        public int ProjectId { get; set; }
        public Project project { get; set; }

        [ForeignKey("bidder")]
        public string BidderId { get; set; }
        public User bidder { get; set; }

        public string Status { get; set; }
        public string Note { get; set; }
        public string Proposal { get; set; }
        public string Timeline { get; set; }

        [Required (ErrorMessage = "Bid amount required")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Bid must be a positive number")]
        public decimal Bid { get; set; }
        public DateTime SubmittedTime { get; set; }

    }
}
