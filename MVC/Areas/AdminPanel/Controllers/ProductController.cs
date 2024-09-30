using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using MVC.Areas.AdminPanel.Models.VMs;
using MVC.Areas.AdminPanel.Models.VMs.Category;
using MVC.Areas.AdminPanel.Models.VMs.Product;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]

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


        public async Task<IActionResult> UploadProductImage(int ProductId)
        {
            var response = await _httpClient.GetAsync($"{uri}/Product/FindProduct/{ProductId}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadAsStringAsync();
                bool productExists = bool.Parse(product); // veya JsonConvert.DeserializeObject<bool>(content) kullanabilirsiniz

                if (productExists)
                {
                    var vm = new UploadProductImageVM
                    {
                        ProductId = ProductId
                    };
                    return View(vm);
                }
            }
            return NotFound();
        }

        //ürüne resim ekleme metodu
        [HttpPost]
        public async Task<IActionResult> UploadProductImage(UploadProductImageVM vm)
        {
            if (vm.File == null || vm.File.Length == 0)
                return View("Error", new ErrorViewModel { RequestId = "No file uploaded." });

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(vm.File.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(vm.File.ContentType);
                content.Add(fileContent, "file", vm.File.FileName);

                try
                {
                    var response = await _httpClient.PostAsync($"{uri}/product/UploadProductImage/{vm.ProductId}", content);
					var jsonResponse = await response.Content.ReadAsStringAsync();
					var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
					var imageUrl = responseData.imageUrl.ToString();
                    await Console.Out.WriteLineAsync(imageUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        // Hata mesajını okuyun ve hata sayfasına gönderin
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return View("Error", new ErrorViewModel
                        {
                            RequestId = $"File upload failed. Server response: {errorMessage}"
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluşursa hata sayfasına yönlendirin
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = $"An error occurred: {ex.Message}"
                    });
                }
            }

            // Başarılı yüklemeden sonra detay sayfasına yönlendirin
            return RedirectToAction("GetAllProducts","Product",new {area="AdminPanel"});
      
        }





        //ürün ekleme
        public async Task<IActionResult> Create()
        {
            CreateProductVM Createvm = new CreateProductVM();
            var response = await _httpClient.GetAsync($"{uri}/Category/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                Createvm.Categories = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            }
            var response1 = await _httpClient.GetAsync($"{uri}/Product/GetAllMaterials");
            if (response1.IsSuccessStatusCode)
            {
                var materials = await response1.Content.ReadFromJsonAsync<List<MaterialVM>>();
                Createvm.Materials = materials.Select(x => new SelectListItem() { Text = x.Name, Value = x.MaterialId.ToString() }).ToList();
            }
            return View(Createvm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM vm)
        {
            var response = await _httpClient.GetAsync($"{uri}/Category/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                foreach (var item in categories)
                {
                    if (vm.CreateProduct.CategoryId == item.CategoryId)
                    {
                        Category category = new Category();
                        category.CategoryId = item.CategoryId;
                        category.CategoryName = item.CategoryName;
                    }
                }
                vm.Categories = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            }
            var response1 = await _httpClient.GetAsync($"{uri}/Product/GetAllMaterials");
            if (response1.IsSuccessStatusCode)
            {
                var materials = await response1.Content.ReadFromJsonAsync<List<MaterialVM>>();
                foreach (var item in materials)
                {
                    if (vm.CreateProduct.MaterialId == item.MaterialId)
                    {
                        MaterialVM materialVM = new MaterialVM();
                        materialVM.MaterialId = item.MaterialId;
                        materialVM.Name = item.Name;
                    }
                }
                vm.Materials = materials.Select(x => new SelectListItem() { Text = x.Name, Value = x.MaterialId.ToString() }).ToList();
            }

            var response2 = await _httpClient.PostAsJsonAsync($"{uri}/Product/AddProduct", vm.CreateProduct);
            if (response2.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return View(vm);

        }

        UpdateProductVM Updatevm = new UpdateProductVM();
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
                Updatevm.Materials = materials.Select(x => new SelectListItem() { Text = x.Name, Value = x.MaterialId.ToString() }).ToList();
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
        public async Task<IActionResult> ActiveProduct(int ProductId)
        {
            var response = await _httpClient.GetAsync($"{uri}/Product/MakeProductActive/{ProductId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProducts");
            }
            return NotFound();
        }


        //ürün detayını gösterme
        public async Task<IActionResult> Details(int ProductId)
        {
            var reponse = await _httpClient.GetAsync($"{uri}/Product/GetProductById/{ProductId}");
            if (reponse.IsSuccessStatusCode)
            {
                var product = await reponse.Content.ReadFromJsonAsync<GetProductDetailsVM>();
                return View(product);
            }
            return NotFound();
        }






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
