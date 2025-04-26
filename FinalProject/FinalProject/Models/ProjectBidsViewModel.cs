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
        public decimal BidValue {  get; set; }
        public DateTime SubmittedTime { get; set; }

    }
}