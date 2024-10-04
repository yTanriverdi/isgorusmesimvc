namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class UploadProductImageVM
    {
        public int ProductId { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
