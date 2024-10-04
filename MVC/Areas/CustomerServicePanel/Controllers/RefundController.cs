using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.DTOs.OfferCart;

namespace MVC.Areas.CustomerServicePanel.Controllers
{
    [Area("CustomerServicePanel")]
    [Authorize(Roles = "CustomerService")]
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

        [HttpGet]
        public async Task<IActionResult> RefundRequestAccept(int offerCartId)
        {
            await _httpClient.PutAsJsonAsync($"{uri}/OfferCart/OfferCartRefundRequestAcceptCustomerService/{offerCartId}", new { });
            return RedirectToAction(nameof(GetAllRefundRequestByCustomerService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRefundRequestByCustomerService()
        {
            var response = await _httpClient.GetAsync($"{uri}/OfferCart/AllRefundRequestOfferCardsByCustomerService");
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<OfferCartDTO> refundOfferCarts = await response.Content.ReadFromJsonAsync<IEnumerable<OfferCartDTO>>();
                return View(refundOfferCarts);
            }
            return NotFound("İade edilmek istenen siparişiniz yok || ÇOK İYİSİNİZ !!!!  :)");
        }

        [HttpGet]
        public async Task<IActionResult> AllCompletedRefundRequest()
        {
            var response = await _httpClient.GetAsync($"{uri}/OfferCart/AllCompletedRefundRequest");
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<OfferCartDTO> refundOfferCarts = await response.Content.ReadFromJsonAsync<IEnumerable<OfferCartDTO>>();
                return View(refundOfferCarts);
            }
            return NotFound("Henüz iadesi tamamlanmış bir iade işleminiz yok..");
        }

        [HttpGet]
        public async Task<IActionResult> RefundRequestDecline(int offerCartId)
        {
            await _httpClient.PutAsJsonAsync($"{uri}/OfferCart/OfferCartRefundRequestDeclineCustomerService/{offerCartId}", new { });
            return RedirectToAction(nameof(GetAllRefundRequestByCustomerService));
        }
    }
}
