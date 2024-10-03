using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.VMs.Report;
using MVC.Areas.ContentManagerPanel.Models.DTOs.Blog;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReportController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5211/api/Report";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOfferCartSummaryReport(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetOfferCartSummaryReport?startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetOfferCartSummaryReportVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetUserActivityReport(int userId)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetUserActivityReport/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetUserActivityReportVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetProductOfferStatusReport()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetProductOfferStatusReport");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetProductOfferStatusReportVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetApprovedOfferCartReport(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetApprovedOfferCartReport?startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetOfferCartSummaryReportVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetPaymentReport()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetPaymentReport");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetPaymentReportVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetRejectedOffersReport()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetRejectedOffersReport");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetRejectedOffersReportVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetMoldProductionReport()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetMoldProductionReport");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetMoldProductionReportVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetProductAccordingToLike()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetProductAccordingToLike");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetProductAccordingToLikeVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

        public async Task<IActionResult> GetProductAccordingToViews()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetProductAccordingToViews");
            if (response.IsSuccessStatusCode)
            {
                var offerCarts = await response.Content.ReadFromJsonAsync<List<GetProductAccordingToViewsVM>>();
                return View(offerCarts);
            }
            return NoContent();
        }

    }
}





