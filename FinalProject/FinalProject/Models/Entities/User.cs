using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Entities
{
    public class User
    {
        //Define Keys, Foreign Keys, and Rows
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required."), StringLength(20)]
        public string Name { get; set; }

        public string? ProfilePicture { get; set; }

        [Required(ErrorMessage = "Bio is required."),StringLength(100)]
        public string Bio { get; set; }
        [Required(ErrorMessage = "Email is required."),StringLength(20)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required."), StringLength(12), MinLength(10)]
        public string Phone { get; set; }
    }
}
