﻿namespace MVC.Models.DTOs
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Kullanıcının rol bilgisi
    }
}
