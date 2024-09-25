using MVC.Areas.AdminPanel.Models.VMs.User;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
