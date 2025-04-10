using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectAuthentication.Models
{
    public class AssignRoleViewModel
    {
        public string AppUserId { get; set; }
        public string? AppUserName { get; set; }
        public List<SelectListItem>? AppUsers { get; set; }
        public List<SelectListItem>? Roles { get; set; }
        public List<string>? SelectedRoles { get; set; }
    }
}
