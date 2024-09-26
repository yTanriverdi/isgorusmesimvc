
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.User;
using MVC.Models.DTOs;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize("Admin")]

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


        string uri = "http://localhost:5211/api/User";
        //string uri = "https://api20240918230552.azurewebsites.net//api/User";

        /// <summary>
        /// Sisteme kayıt olan tüm kullanıcıları rollerine göre listeleme metodu
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllUserByRoles()
        {
            var users = await _httpClient.GetAsync($"{uri}/GetAllUsers");
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
            var user = await _httpClient.GetAsync($"{uri}/GetUserDetail/{userId}");
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
            var user = await _httpClient.PutAsJsonAsync($"{uri}/UpdateAdmin/{userId}", updateUserVM);
            if (user.IsSuccessStatusCode)
                return RedirectToAction("GetAllUserByRoles");
            return View(updateUserVM);
        }

    }
}
