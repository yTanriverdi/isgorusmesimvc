using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class UpdateProductVM
    {
        public UpdateProductDTO UpdateProductDTO { get; set; }
        public List<SelectListItem> Categories { get; set; }    //kategorilerin tümünü listeler
        public List<SelectListItem> Materials { get; set; }
    }
}
