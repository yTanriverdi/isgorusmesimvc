using MVC.Areas.AdminPanel.Models.VMs.User;

namespace MVC.Areas.ContentManagerPanel.Models.DTOs.Blog
{
    public class AddBlogDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? SourceUrl { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
