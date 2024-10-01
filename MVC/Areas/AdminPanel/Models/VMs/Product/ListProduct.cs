namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class ListProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Likes { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
