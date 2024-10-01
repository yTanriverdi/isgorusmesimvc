using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.OfferCart;
using MVC.Areas.AdminPanel.Models.VMs.Product;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class OfferCartController : Controller
    {
        private readonly HttpClient _httpClient;

        public OfferCartController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5211/api/OfferCart";

        public async Task<IActionResult> GetAllOfferCarts()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetAllOfferCarts");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetAllOfferCartsVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }
    }
}
