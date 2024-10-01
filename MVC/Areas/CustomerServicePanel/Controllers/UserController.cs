using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.User;
using System.Net.Http;

namespace MVC.Areas.CustomerServicePanel.Controllers
{

    [Area("CustomerServicePanel")]
    [Authorize(Roles = "CustomerService")]
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


        //Kullanıcı bilgisi detayını gösterme
        public async Task<IActionResult> GetUserDetails(int Id)
        {
            var user = await _httpClient.GetAsync($"{uri}/User/GetUserDetail/{Id}");
            if (user.IsSuccessStatusCode)
            {
                var userDetail = await user.Content.ReadFromJsonAsync<UserDetailVM>();
                return View(userDetail);
            }
            return View();
        }


        //sistemdeki kullanıcının kayıtlı bilgilerini güncelleme
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
        public async Task<IActionResult> UpdateAdmin(UpdateUserVM updateUserVM)
        {
            var user = await _httpClient.PutAsJsonAsync($"{uri}/User/UpdateUser/{updateUserVM.Id}", updateUserVM);
            if (user.IsSuccessStatusCode)
                return RedirectToAction("GetUserDetails");
            return View(updateUserVM);
        }









    }
}
