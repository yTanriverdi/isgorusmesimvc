using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.User;
using MVC.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using MVC.Models.DTOs;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
        }

        string uri = "http://localhost:5211/api/User";

        //kullanıcı girişi
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.GetAsync($"{uri}/LoginUser/{vm.Email}/{vm.Password}");
                if (response.IsSuccessStatusCode)
                {
                    var userLoginResult = await response.Content.ReadFromJsonAsync<UserLoginDTO>();

                    // Eğer kullanıcı bulunmuş ve şifre doğruysa
                    if (userLoginResult != null)
                    {
                        // Kullanıcı için claims listesi oluştur
                        List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userLoginResult.AppUser.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, userLoginResult.AppUser.Id.ToString()),
                    new Claim("Id", userLoginResult.AppUser.Id.ToString()),
                    new Claim(ClaimTypes.Role,userLoginResult.UserRoles.FirstOrDefault())
                };

                        // Claims identity ve authentication properties oluştur
                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties()
                        {
                            IsPersistent = true,  // Oturumu kalıcı yap (çerez ile)
                            ExpiresUtc = DateTime.Now.AddMinutes(30),  // Oturumun süresi
                        };

                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        // Oturumu başlat
                        await HttpContext.SignInAsync(principal, properties);

                        //rolü neyse o areaya yönlendiriyor
                        if (userLoginResult.UserRoles.Contains("Admin"))
                            return RedirectToAction("Create", "Product", new { area = "AdminPanel" });
                        //return RedirectToAction("Index", "Home");
                        else if (userLoginResult.UserRoles.Contains("ContentManager"))
                            return RedirectToAction("GetAllProducts", "Product", new { area = "ContentManagerPanel" });
                        else if (userLoginResult.UserRoles.Contains("CustomerService"))
                            return RedirectToAction("Index", "Home", new { area = "CustomerServicePanel" });
                        else if (userLoginResult.UserRoles.Contains("Visitor"))
                            return RedirectToAction("Index", "Home", new { area = "Visitor" });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email veya şifre hatalı.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "API isteği başarısız.");
                }
            }
            return View(vm); //giriş başarısız olursa da aynı sayfada bizi tutacak tüm bilgilerle
        }




        //kullanıcı kayıt olma
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM user)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync($"{uri}/CreateUser", user);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı oluşturulurken bir hata meydana geldi");
                return BadRequest(ModelState);
            }
            return View(user);
        }


        //kullanıcı çıkış

        public async Task<IActionResult> Logout()
        {
            var response = await _httpClient.GetAsync($"{uri}/Logout");

            if (response.IsSuccessStatusCode)
            {
                _contextAccessor.HttpContext.Session.Clear();
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction("Index", "Home");
            }
            return BadRequest();
        }

    }
}
