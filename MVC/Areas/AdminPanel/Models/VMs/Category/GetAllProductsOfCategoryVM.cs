
namespace MVC.Areas.AdminPanel.Models.VMs.Category
{
    public class GetAllProductsOfCategoryVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
