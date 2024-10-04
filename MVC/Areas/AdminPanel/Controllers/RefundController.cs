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

        string uri = "http://localhost:5211/api";

        [HttpGet]
        public async Task<IActionResult> RefundRequestAccept(int offerCartId)
        {
            await _httpClient.PutAsJsonAsync($"{uri}/OfferCart/OfferCartRefundRequestAcceptAdmin/{offerCartId}", new { });
            return RedirectToAction(nameof(GetAllRefundRequestByAdmin));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRefundRequestByAdmin()
        {
            var response = await _httpClient.GetAsync($"{uri}/OfferCart/AllRefundRequestOfferCardsByAdmin");
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<OfferCartDTO> refundOfferCarts = await response.Content.ReadFromJsonAsync<IEnumerable<OfferCartDTO>>();
                return View(refundOfferCarts);
            }
            return NoContent();
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
            await _httpClient.PutAsJsonAsync($"{uri}/OfferCart/OfferCartRefundRequestDeclineAdmin/{offerCartId}", new { });
            return RedirectToAction(nameof(GetAllRefundRequestByAdmin));
        }
    }
}
