using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.AdminPanel.Models.VMs;
using MVC.Areas.AdminPanel.Models.VMs.Category;
using MVC.Areas.AdminPanel.Models.VMs.Product;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

namespace MVC.Areas.ContentManagerPanel.Controllers
{
    [Area("ContentManagerPanel")]
    [Authorize(Roles = "ContentManager")]

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
                var products = await response.Content.ReadFromJsonAsync<IEnumerable<ListProduct>>();
                return View(products);
            }
            return NoContent();
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
            var response2 = await _httpClient.PostAsJsonAsync($"{uri}/Product/AddProduct", vm.AddProductDTO);
            if (response2.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ürün başarıyla eklendi.";
                return RedirectToAction("GetAllProducts");
            }
            TempData["ErrorMessage"] = "Ürün ekleme başarısız!";
            return View(vm);
        }

        UpdateProductVM Updatevm = new UpdateProductVM();
        //ürün güncelleme
        public async Task<IActionResult> Update(int ProductId)
        {

            var response = await _httpClient.GetAsync($"{uri}/Product/GetProductById/{ProductId}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<UpdateProductDTO>();
                Updatevm.UpdateProductDTO = product;
            }
            var response1 = await _httpClient.GetAsync($"{uri}/Category/GetAllCategories");
            if (response1.IsSuccessStatusCode)
            {
                var categories = await response1.Content.ReadFromJsonAsync<List<Category>>();
                Updatevm.Categories = categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            }
            var response2 = await _httpClient.GetAsync($"{uri}/Product/GetAllMaterials");
            if (response2.IsSuccessStatusCode)
            {
                var materials = await response2.Content.ReadFromJsonAsync<List<MaterialVM>>();
                Updatevm.Materials = materials.Select(x => new SelectListItem() { Text = x.Name, Value = x.MaterialId.ToString() }).ToList();
            }

            Updatevm.UpdateProductDTO.ProductId = ProductId;
            return View(Updatevm);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM vm)
        {
            var response3 = await _httpClient.PutAsJsonAsync($"{uri}/Product/UpdateProduct", vm.UpdateProductDTO);
            if (response3.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ürün başarıyla güncellendi.";
                return RedirectToAction("GetAllProducts");
            }
            TempData["ErrorMessage"] = "Güncelleme başarısız!";
            return View(vm);
        }


        //ürün pasif etme(silme)
        public async Task<IActionResult> Delete(int ProductId)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/Product/DeleteProduct/{ProductId}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
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
                var products = await response.Content.ReadFromJsonAsync<List<AddProductDTO>>();
                return View(products);
            }
            return NotFound();
        }


        //***************************************************************


        //yeni resim ekleme deneme metodu
        public async Task<IActionResult> UpdateProductImage(int productId)
        {
            var response = await _httpClient.GetAsync($"{uri}/Product/FindProduct/{productId}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadAsStringAsync();
                bool productExists = bool.Parse(product);

                if (productExists)
                {
                    var vm = new UploadProductImageVM
                    {
                        ProductId = productId
                    };
                    return View(vm);
                }
            }
            return NotFound();
        }

		[HttpPost]
		public async Task<IActionResult> UpdateProductImage(UploadProductImageVM vm)
		{
			if (vm.Files == null || !vm.Files.Any())
			{
				TempData["ErrorMessage"] = "Lütfen en az bir resim dosyası seçin!";
				return View(vm);
			}

			List<string> uploadedImageUrls = new List<string>();

			// Her dosya için işlemi gerçekleştir
			foreach (var file in vm.Files)
			{
				if (file.Length > 0)
				{
					string path = Path.Combine("wwwroot/images", file.FileName);
					using (FileStream fs = new FileStream(path, FileMode.Create))
					{
						await file.CopyToAsync(fs);
					}

					uploadedImageUrls.Add(file.FileName); // Yüklenen resim URL'sini listeye ekle
				}
			}

			// UploadImage nesnesini oluştur ve API'ye gönder
			UploadImage uploadImage = new UploadImage
			{
				ProductId = vm.ProductId,
				ImageUrls = uploadedImageUrls // Birden fazla URL'yi liste olarak API'ye gönder
			};

			var response = await _httpClient.PostAsJsonAsync($"{uri}/Product/UpdateProductImage", uploadImage); // API'ye çoklu resimleri gönder
			if (response.IsSuccessStatusCode)
			{
				TempData["SuccessMessage"] = "Resimler başarıyla yüklendi!";
				return RedirectToAction("GetAllProducts");
			}

			TempData["ErrorMessage"] = "Resim yükleme başarısız oldu.";
			return View(vm);

		}














	}
}
