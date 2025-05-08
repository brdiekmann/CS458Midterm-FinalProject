using FinalProject.Models;
using FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinalProject.Models
{
    public class ProjectBidsViewModel
    {
        [Required(ErrorMessage = "Project is required")]
        public int ProjectId { get; set; }
        [ValidateNever]
        public Project project { get; set; }

        [Required(ErrorMessage = "Bidder is required")]
        public string BidderId { get; set; }
        [ValidateNever]
        public User bidder { get; set; }

        [Required(ErrorMessage = "Bid amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid must be a positive number")]
        public decimal BidValue {  get; set; }

        [Required(ErrorMessage = "Submission time is required")]
        public DateTime SubmittedTime { get; set; }

    }
}