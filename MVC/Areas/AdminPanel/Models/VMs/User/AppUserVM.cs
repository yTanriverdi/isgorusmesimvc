using Microsoft.AspNetCore.Identity;

namespace MVC.Areas.AdminPanel.Models.VMs.User
{
    public class AppUserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
