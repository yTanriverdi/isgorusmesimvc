namespace MVC.Areas.ContentManagerPanel.Models.DTOs.Blog
{
    public class UpdateBlogDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? SourceUrl { get; set; }
    }
}
