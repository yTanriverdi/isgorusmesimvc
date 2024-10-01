namespace MVC.Areas.AdminPanel.Models.VMs.OfferCart
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Likes { get; set; }
        public int? Views { get; set; }
    }
}
