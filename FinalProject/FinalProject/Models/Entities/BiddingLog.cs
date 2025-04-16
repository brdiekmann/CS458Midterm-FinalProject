using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class BiddingLog
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        [ForeignKey("operatorId")]
        public string OperatorId { get; set; }
        public User operatorId { get; set; }

        public string Operation { get; set; }
        public string Note { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey("projectBid")]
        public int ProjectBidId { get; set; }
        public ProjectBid projectBid { get; set; }
    }
}
