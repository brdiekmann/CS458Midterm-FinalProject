﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
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
        public int BidderId { get; set; }
        public User bidder { get; set; }

        public string Status { get; set; }
        public string Note { get; set; }
        public string Proposal { get; set; }
        public string Timeline { get; set; }
        public DateTime SubmittedTime { get; set; }

    }
}
