using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class User
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        [Required,StringLength(100)]
        public string Bio { get; set; }
        [Required,StringLength(20)]
        public string Email { get; set; }
        [Required, StringLength(12), MinLength(10)]
        public string Phone { get; set; }
    }
}
