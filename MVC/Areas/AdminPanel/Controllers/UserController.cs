
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.AdminPanel.Models.VMs.User;
using MVC.Models.DTOs;
using System.Text.Json;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }


        string uri = "http://localhost:5211/api";
        //string uri = "https://api20240918230552.azurewebsites.net//api/User";

        /// <summary>
        /// Sisteme kayıt olan tüm kullanıcıları rollerine göre listeleme metodu
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllUserByRoles()
        {
            var users = await _httpClient.GetAsync($"{uri}/User/GetAllUsers");
            if (users.IsSuccessStatusCode)
            {
                var userList = await users.Content.ReadFromJsonAsync<List<UserDetailVM>>();
                var userByRoles = userList.GroupBy(x => x.Role).ToDictionary(x => x.Key, a => a.ToList());
                return View(userByRoles);   //rollere göre kullanıcıları gruplayıp view de listeliyoruz
            }
            return View();
        }


        /// <summary>
        /// Tek bir kullanıcının detayını görüntüleme
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUserDetails(int userId)
        {
            var user = await _httpClient.GetAsync($"{uri}/User/GetUserDetail/{userId}");
            if (user.IsSuccessStatusCode)
            {
                var userDetail = await user.Content.ReadFromJsonAsync<UserDetailVM>();
                return View(userDetail);
            }
            return View();
        }


        [HttpGet]
        public IActionResult UpdateAdmin(int userId)
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> UpdateAdmin(int userId, UpdateUserVM updateUserVM)
        {
            var user = await _httpClient.PutAsJsonAsync($"{uri}/User/UpdateAdmin/{userId}", updateUserVM);
            if (user.IsSuccessStatusCode)
                return RedirectToAction("GetAllUserByRoles");
            return View(updateUserVM);
        }

        public async Task<IEnumerable<AppRole>> GetAllRolesAsync()
        {
            var response = await _httpClient.GetAsync($"{uri}/Role/GetAllRoles");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                // JSON'dan AppRole listesine dönüştür
                var roles = JsonSerializer.Deserialize<List<AppRole>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // JSON isimlendirme politikasına göre
                });

                return roles ?? new List<AppRole>(); // Boş liste döndür
            }
            return Enumerable.Empty<AppRole>(); // Başarısız olursa boş liste döndür
        }

        //kullanıcı rolünü değiştirme
        public async Task<IActionResult> ChangeUserRole(int Id)
        {
            var response = await _httpClient.GetAsync($"{uri}/User/GetUserDetail/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDetailVM>();

                var roles = await GetAllRolesAsync();
                ViewBag.Roles = roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                ViewBag.UserRoles = user.Role;
                return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(int Id, AppUser model)
        {
            if (!string.IsNullOrEmpty(model.SelectedRoleId.ToString()))
            {
                var request = new ChangeUserRoleVM
                {
                    UserId = Id,
                    NewRole = model.SelectedRoleId
                };
                var response = await _httpClient.PutAsJsonAsync($"{uri}/Role/ChangeUserRole", request);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllUserByRoles");
                }
            }
            return BadRequest();
        }










    }
}
