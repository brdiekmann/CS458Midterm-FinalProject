using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class User
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
