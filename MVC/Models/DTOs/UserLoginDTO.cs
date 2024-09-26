using MVC.Areas.AdminPanel.Models.VMs.User;

namespace MVC.Models.DTOs
{
    public class UserLoginDTO
    {
        public AppUser AppUser { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
        public bool Error { get; set; }
    }
}
