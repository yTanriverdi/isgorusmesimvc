using ApplicationLayer.Models.DTOs;
using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs;

namespace MVC.Controllers
{
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


        //string uri = "http://localhost:5211/api/User";
        string uri = "https://api20240918230552.azurewebsites.net//api/User";

        /// <summary>
        /// Sisteme kayıt olan tüm kullanıcıları rollerine göre listeleme metodu
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllUserByRoles()
        {
            var users=await _httpClient.GetAsync($"{uri}/GetAllUsers");
            if(users.IsSuccessStatusCode)
            {
                var userList = await users.Content.ReadFromJsonAsync<List<UserDetailDTO>>();
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
                var userDetail = await user.Content.ReadFromJsonAsync<UserDetailDTO>();
                return View(userDetail);
            }
            return View();
        }



        //public async Task<IActionResult> UpdateAdmin(int userId)
        //{
        //    var user = await _httpClient.PutAsJsonAsync($"{uri}/UpdateAdmin/{userId}");
        //}













    }
}
