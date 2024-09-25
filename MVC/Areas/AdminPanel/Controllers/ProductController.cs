using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.AdminPanel.Models.VMs;
using MVC.Areas.AdminPanel.Models.VMs.Category;
using MVC.Areas.AdminPanel.Models.VMs.Product;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize("Admin")]

    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5211/api";

        //Tüm ürünleri listeleme
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _httpClient.GetAsync($"{uri}/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<List<ListProduct>>();
                return View(products);
            }
            return NoContent();
        }

        CreateProductVM Createvm = new CreateProductVM();

        //ürün ekleme
        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAsync($"{uri}/Category/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                Createvm.Categories = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            }
            var response1 = await _httpClient.GetAsync($"{uri}/Material/GetlAllMaterials/");
            if (response1.IsSuccessStatusCode)
            {
                var materials = await response.Content.ReadFromJsonAsync<List<MaterialVM>>();
                Createvm.Materials = materials.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }
            return View(Createvm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM vm)
        {
            //if (vm.Image != null)
            //{
            //    string path = "wwwroot/ProductPictures/" + vm.Image.FileName;
            //    FileStream fs = new FileStream(path, FileMode.Create);
            //    await vm.Image.CopyToAsync(fs);
            //    fs.Close();
            //}
            var response = await _httpClient.PostAsJsonAsync($"{uri}/Product/AddProduct", vm.CreateProduct);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return View(vm);
        }

        UpdateProductVM Updatevm=new UpdateProductVM();
        //ürün güncelleme
        public async Task<IActionResult> Update(int ProductId)
        {
            var response = await _httpClient.GetAsync($"{uri}/Product/GetProductById/{ProductId}");
            if (response.IsSuccessStatusCode)
            {
                Updatevm.UpdateProduct = await response.Content.ReadFromJsonAsync<UpdateProduct>();
            }
            var response1 = await _httpClient.GetAsync($"{uri}/Category/GetAllCategories");
            if (response1.IsSuccessStatusCode)
            {
                var categories = await response1.Content.ReadFromJsonAsync<List<Category>>();
                Updatevm.Categories = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            }
            var response2 = await _httpClient.GetAsync($"{uri}/Material/GetlAllMaterials/");
            if (response2.IsSuccessStatusCode)
            {
                var materials = await response2.Content.ReadFromJsonAsync<List<MaterialVM>>();
                Updatevm.Materials = materials.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }
            return View(Updatevm);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int ProductId, UpdateProductVM vm)
        {
            var response = await _httpClient.PutAsJsonAsync($"{uri}/UpdateProduct/{ProductId}", vm.UpdateProduct);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return View(vm);
        }



        //ürün pasif etme(silme)
        public async Task<IActionResult> Delete(int ProductId)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/Product/DeleteProduct/{ProductId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return NotFound();
        }


        //Ürün aktif etme(metodunu repo-servise-api'de yazman lazım.Sonra burada oluştur)










        //Arama motorundaki kelimeye göre kategroileri bulup getirme
        public async Task<IActionResult> SearchCategoryByKeyword(string keyword)
        {
            var response = await _httpClient.GetAsync($"{uri}/Product/{keyword}");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<List<CreateProduct>>();
                return View(products);
            }
            return NotFound();
        }














    }
}
