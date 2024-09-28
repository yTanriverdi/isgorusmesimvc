﻿namespace MVC.Areas.AdminPanel.Models.VMs.User
{
    public class UserDetailVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Kullanıcının rol bilgisi
        public int SelectedRoleId { get; set; }

    }
}
