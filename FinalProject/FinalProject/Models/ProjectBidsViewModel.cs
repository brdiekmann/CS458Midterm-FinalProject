using FinalProject.Models;
using FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class ProjectBidsViewModel
    {
        public int ProjectId { get; set; }
        public Project project { get; set; }
        public string BidderId { get; set; }
        public User bidder { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Proposal { get; set; }
        public string Timeline { get; set; }
        public decimal Bid {  get; set; }
        public DateTime SubmittedTime { get; set; }

    }
}