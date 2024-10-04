namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class ListProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Likes { get; set; }
        public List<string> ImageUrls { get; set; }
        public bool IsActive { get; set; }
    }
}
