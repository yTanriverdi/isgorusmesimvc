using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.Category;
using MVC.Areas.AdminPanel.Models.VMs.Product;
using MVC.Areas.ContentManagerPanel.Models.DTOs.Blog;
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
    }
}
