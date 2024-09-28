namespace MVC.Areas.AdminPanel.Models.VMs.Category
{
    public class Category
    {

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product>? Products { get; set; }

        //public DateTime AddedDate { get; set; } = DateTime.Now;
        //public DateTime? UpdateDate { get; set; }
        //public DateTime? DeleteDate { get; set; }
        //public bool IsActive { get; set; }
    }
}
