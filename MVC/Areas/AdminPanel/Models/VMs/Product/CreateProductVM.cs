using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.AdminPanel.Models.VMs.Category;

namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class CreateProductVM
    {
        public CreateProduct CreateProduct { get; set; }      
        public List<SelectListItem> Categories { get; set; }    //kategorilerin tümünü listeler
        public List<SelectListItem> Materials { get; set; }

        //public IFormFile Image { get; set; }    //fotoğraf güncelleme metodu yapılırsa buna gerek kalmayacak
    }
}
