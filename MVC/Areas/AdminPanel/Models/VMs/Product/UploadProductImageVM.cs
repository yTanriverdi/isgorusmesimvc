namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class UploadProductImageVM
    {
        public int ProductId { get; set; }
        public IFormFile File { get; set; }
    }
}
