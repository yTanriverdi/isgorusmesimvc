
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.AdminPanel.Models.VMs.User;
using MVC.Models.DTOs;
using System.Text.Json;

namespace MVC.Areas.ContentManagerPanel.Controllers
{
    [Area("ContentManagerPanel")]
    [Authorize(Roles = "ContentManager")]

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
        public async Task<IActionResult> UpdateAdmin(int userId)
        {
            var user = await _httpClient.GetAsync($"{uri}/User/GetUserDetail/{userId}");
            if (user.IsSuccessStatusCode)
            {
                var userDetail = await user.Content.ReadFromJsonAsync<UpdateUserVM>();
                return View(userDetail);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdmin( UpdateUserVM updateUserVM)
        {
            var user = await _httpClient.PutAsJsonAsync($"{uri}/User/UpdateAdmin/{updateUserVM.Id}", updateUserVM);
            if (user.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kullanıcı bilgileri başarıyla güncellendi.";
                return RedirectToAction("GetUserDetails");
            }
            TempData["ErrorMessage"] = "Kullanıcı bilgileri güncellenemedi!";
            return View(updateUserVM);
        }









    }
}
