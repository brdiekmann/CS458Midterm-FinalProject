using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class Attachment
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Type is required")]
        [StringLength(100,ErrorMessage ="Type must be be less than 100 characters")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be be less than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [StringLength(200)]
        public string Location { get; set; }
        [Required(ErrorMessage = "Last modified timestamp is required")]
        public DateTime LastModifiedTimestamp { get; set; }

        [ForeignKey("project")]
        [Required(ErrorMessage="ProjectId is required")]
        [Range(0,int.MaxValue,ErrorMessage = "Id is outside the bound of the range")]
        public int ProjectId { get; set; }
        public Project? project { get; set; }
    }
}
