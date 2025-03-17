using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class Attachment
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime LastModifiedTimestamp { get; set; }

        [ForeignKey("project")]
        public int ProjectId { get; set; }
        public Project project { get; set; }
    }
}
