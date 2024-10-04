using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.Category;
using MVC.Areas.AdminPanel.Models.VMs.Product;
using MVC.Areas.ContentManagerPanel.Models.DTOs.Blog;
using MVC.Areas.ContentManagerPanel.Models.VMs.Blog;
using MVC.Models.DTOs;
using System.Security.Claims;

namespace MVC.Areas.ContentManagerPanel.Controllers
{
    [Area("ContentManagerPanel")]
    [Authorize(Roles = "ContentManager")]
    public class BlogController : Controller
    {
        private HttpClient _httpClient;

        public BlogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5211/api/Blog";

        public async Task<IActionResult> GetAllBlogs()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetAllBlogs");
            if (response.IsSuccessStatusCode)
            {
                var blogs = await response.Content.ReadFromJsonAsync<List<BlogDTO>>();
                return View(blogs);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBlogDTO addBlogDto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                addBlogDto.AppUserId = userId;
            }

            var response = await _httpClient.PostAsJsonAsync($"{uri}/AddBlog", addBlogDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllBlogs));
            }
            return View(addBlogDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetBlogById/{id}");
            if (response.IsSuccessStatusCode)
            {
                UpdateBlogDTO updateBlogDto = await response.Content.ReadFromJsonAsync<UpdateBlogDTO>();
                updateBlogDto.BlogId = id;
                return View(updateBlogDto);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateBlogDTO updateBlogDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{uri}/UpdateBlog/{id}", updateBlogDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllBlogs));
            }
            return View(updateBlogDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/DeleteBlog/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllBlogs));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            var response = await _httpClient.GetAsync($"{uri}/SearchBlogTitleByKeyword/{keyword}");
            if (response.IsSuccessStatusCode)
            {
                List<BlogDTO> listBlogDto = await response.Content.ReadFromJsonAsync<List<BlogDTO>>();
                return View(listBlogDto);
            }
            return BadRequest();
        }

        //resim yükleme metodu
        public async Task<IActionResult> UpdateBlogImage(int BlogId)
        {
            var response = await _httpClient.GetAsync($"{uri}/FindBlog/{BlogId}");
            if (response.IsSuccessStatusCode)
            {
                var blog = await response.Content.ReadAsStringAsync();
                bool blogExists = bool.Parse(blog);

                if (blogExists)
                {
                    var vm = new BlogImageVM
                    {
                        BlogId = BlogId
                    };
                    return View(vm);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlogImage(BlogImageVM vm)
        {
            if (vm.File == null || vm.File.Length == 0)
            {
                TempData["ErrorMessage"] = "Lütfen bir resim dosyası seçin!";
                return View(vm);
            }
            string path = "wwwroot/BlogImages/" + vm.File.FileName;
            FileStream fs = new FileStream(path, FileMode.Create);
            await vm.File.CopyToAsync(fs);
            fs.Close();

            BlogImage uploadImage = new BlogImage();
            uploadImage.BlogId = vm.BlogId;
            uploadImage.ImageUrl = vm.File.FileName;
            var response = await _httpClient.PostAsJsonAsync($"{uri}/UpdateProductImage", uploadImage);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Resim başarıyla yüklendi!";
                return RedirectToAction("GetAllBlogs");
            }
            TempData["ErrorMessage"] = "Lütfen bir resim dosyası seçin!";
            return View(vm);

        }
    }
}
