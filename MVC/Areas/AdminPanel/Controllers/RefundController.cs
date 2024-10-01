using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.DTOs.OfferCart;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class RefundController : Controller
    {
        private readonly HttpClient _httpClient;

        public RefundController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        string uri = "http://localhost:5211/api";

        [HttpPost]
        public async Task<IActionResult> AddRefundRequest(int offerCartId)
        {
            await _httpClient.GetAsync($"{uri}/OfferCart/OfferCartRefundRequest/{offerCartId}");
            return RedirectToAction();
        }

        [HttpPost]
        public async Task<IActionResult> RefundRequestAccept(int offerCartId)
        {
            await _httpClient.GetAsync($"{uri}/OfferCart/OfferCartRefundRequestAccept/{offerCartId}");
            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRefundRequest()
        {
            var response = await _httpClient.GetAsync($"{uri}/OfferCart/GetRefundRequestOfferCards");
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<OfferCartDTO> refundOfferCarts = await response.Content.ReadFromJsonAsync<IEnumerable<OfferCartDTO>>();
                return View(refundOfferCarts);
            }
            return NotFound("İade edilmek istenen siparişiniz yok || ÇOK İYİSİNİZ !!!!  :)");
        }
    }
}
