using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.DTOs.OfferCart;
using MVC.Areas.AdminPanel.Models.VMs.OfferCart;
using MVC.Areas.CustomerServicePanel.Models.DTOs.OfferCart;

namespace MVC.Areas.CustomerServicePanel.Controllers
{
    [Area("CustomerServicePanel")]
    [Authorize(Roles = "CustomerService")]
    public class OfferCartController : Controller
    {
        private readonly HttpClient _httpClient;

        public OfferCartController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        string uri = "http://localhost:5211/api/OfferCart";

        public async Task<IActionResult> GetAllOfferCartsForCustomerService()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetAllOfferCartsForCustomerService");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetAllOfferCartsVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        //sipariş detayını gösterme
        public async Task<IActionResult> Details(int offerCartId)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetOfferCartById/{offerCartId}");
            if (response.IsSuccessStatusCode)
            {
                var offerCart = await response.Content.ReadFromJsonAsync<OfferCartDTO>();
                return View(offerCart);
            }
            return NotFound();
        }

        public async Task<IActionResult> UpdateOfferCart(int offerCartId)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetOfferCartById/{offerCartId}");
            if (response.IsSuccessStatusCode)
            {
                var offerCart = await response.Content.ReadFromJsonAsync<UpdateOfferCartDTO>();
                return View(offerCart);
            }
            return NotFound();
        }

        //Detayların sayfasının içerisinde değişiklikleri onayla butonuyla güncelleme yapan post metodu.
        [HttpPost]
        public async Task<IActionResult> UpdateOfferCart(UpdateOfferCartDTO updateOfferCartDTO)
        {
            var response2 = await _httpClient.PutAsJsonAsync($"{uri}/UpdateOfferCart", updateOfferCartDTO);
            if (response2.IsSuccessStatusCode)
            {
                return RedirectToAction("Details");
            }
            return View(updateOfferCartDTO);
        }

    }

}
