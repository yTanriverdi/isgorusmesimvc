using MVC.Areas.ContentManagerPanel.Models.DTOs.Blog;

namespace MVC.Areas.ContentManagerPanel.Models.VMs.Blog
{
    public class UpdateBlogVM
    {
        public BlogDTO BlogDTO { get; set; }
        public AddBlogDTO AddBlogDTO { get; set; }
        public UpdateBlogDTO UpdateBlogDTO { get; set; }
    }
}
