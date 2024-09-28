
namespace MVC.Areas.AdminPanel.Models.VMs.Category
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        //public int ProductId { get; set; }
        //public string ProductName { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        //public string? ImageUrl { get; set; }
        //public MaterialVM Material { get; set; }
        //public int? Likes { get; set; }
        //public int? Views { get; set; }


        //public DateTime AddedDate { get; set; } = DateTime.Now;
        //public DateTime? UpdateDate { get; set; }
        //public DateTime? DeleteDate { get; set; }
        //public bool IsActive { get; set; }


        ////Navigation Property
        //public int CategoryId { get; set; }
        //public Category? Category { get; set; }
    }
}
