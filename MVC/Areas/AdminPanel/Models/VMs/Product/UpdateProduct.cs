namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class UpdateProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MaterialVM Material { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
    }
}
