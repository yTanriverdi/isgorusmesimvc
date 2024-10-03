using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.DTOs.OfferCartMessage;
using MVC.Areas.AdminPanel.Models.VMs.User;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class OfferCartMessageController : Controller
    {
        private readonly HttpClient _httpClient;

        public OfferCartMessageController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5211/api/OfferCartMessage";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesByOfferCartId(int offerCartId)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetMessagesByOfferCartId/{offerCartId}");
            if (response.IsSuccessStatusCode)
            {
                var offerCartMessageDtos = await response.Content.ReadFromJsonAsync<List<OfferCartMessageDTO>>();
                return View(offerCartMessageDtos);
            }
            
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesByUserId(int appUserId)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetMessagesByUserId/{appUserId}");
            if (response.IsSuccessStatusCode)
            {
                var offerCartMessageDtos = await response.Content.ReadFromJsonAsync<List<OfferCartMessageDTO>>();
                return View(offerCartMessageDtos);
            }
            return NoContent();
        }

    }
}
