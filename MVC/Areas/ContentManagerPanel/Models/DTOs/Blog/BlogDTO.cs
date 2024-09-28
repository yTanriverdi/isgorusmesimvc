using MVC.Areas.AdminPanel.Models.VMs.User;

namespace MVC.Areas.ContentManagerPanel.Models.DTOs.Blog
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? keyword { get; set; } // bu propertyden dolayı vm e çevir.

        public AppUser AppUser { get; set; }
    }
}
