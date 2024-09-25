using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.User;
using MVC.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                    var userLoginResult = await response.Content.ReadFromJsonAsync<AppUserVM>();

                    // Eğer kullanıcı bulunmuş ve şifre doğruysa
                    if (userLoginResult != null)
                    {
                        // Kullanıcı için claims listesi oluştur
                        List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userLoginResult.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, userLoginResult.Id.ToString()),
                    new Claim("Id", userLoginResult.Id.ToString())
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
                        if (userLoginResult.Roles.Contains("Admin"))
                            //return RedirectToAction("Create", "Product", new { area = "AdminPanel" });
                            return RedirectToAction("Index", "Home");
                        else if (userLoginResult.Roles.Contains("ContentManager"))
                            return RedirectToAction("Index", "Home", new { area = "ContentManagerPanel" });
                        else if (userLoginResult.Roles.Contains("CustomerService"))
                            return RedirectToAction("Index", "Home", new { area = "CustomerServicePanel" });
                        else if (userLoginResult.Roles.Contains("Visitor"))
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
                if(response.IsSuccessStatusCode)
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
                return RedirectToAction("Login", "Account");
            }
            return BadRequest();
        }




    }
}
