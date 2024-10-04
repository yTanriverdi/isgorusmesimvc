using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.Category;


namespace MVC.Areas.ContentManagerPanel.Controllers
{
    [Area("ContentManagerPanel")]
    [Authorize(Roles = "ContentManager")]
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5211/api/Category";

        //tüm kategorileri listeler
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                return View(categories);
            }
            return View();
        }

        //kategori id ile o kategoriye ait ürünleri listeleme
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetCategoryById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var categoryData = await response.Content.ReadFromJsonAsync<GetAllProductsOfCategoryVM>();

                return View(categoryData); // Kategori yoksa
            }
            return NotFound(); // API çağrısı başarısızsa
        }

        //kategori ekleme
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync($"{uri}/AddCategory", category);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kategori başarıyla eklendi";
                return RedirectToAction("GetAllCategories");
            }
            TempData["ErrorMessage"] = "Kategori ekleme başarısız!";
            return View(category);
        }



        //kategori güncelleme
        public async Task<IActionResult> Update(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetCategoryById/{id}");
            if (response.IsSuccessStatusCode)
            {
                Category category = await response.Content.ReadFromJsonAsync<Category>();
                category.CategoryId = id;
                return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync($"{uri}/UpdateCategory", category);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kategori başarıyla güncellendi";
                return RedirectToAction("GetAllCategories");
            }
            TempData["ErrorMessage"] = "Kategori güncelleme başarısız!";
            return View(category);
        }


        //kategroiyi pasif etme(silme)
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/DeleteCategory/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kategori başarıyla silindi";
                return RedirectToAction("Index");
            }
            return NotFound();
        }



        //Category aktif etme(metodunu repo-servise-api'de yazman lazım.Sonra burada oluştur)





        //Arama motorundaki kelimeye göre kategroileri bulup getirme
        public async Task<IActionResult> SearchCategoryByKeyword(string keyword)
        {
            var response = await _httpClient.GetAsync($"{uri}/Category/{keyword}");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                return View(categories);
            }
            return NotFound();
        }










    }
}
