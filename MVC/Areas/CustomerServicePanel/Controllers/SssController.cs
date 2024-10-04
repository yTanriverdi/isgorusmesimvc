using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.CustomerServicePanel.Models.DTOs.Sss;

namespace MVC.Areas.CustomerServicePanel.Controllers
{
    [Area("CustomerServicePanel")]
    [Authorize(Roles = "CustomerService")]
    public class SssController : Controller
    {

        private readonly HttpClient _httpClient;

        public SssController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string uri = "http://localhost:5211/api";
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSss()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSss(SssCreateDTO sssCreateDTO)
        {
            var result = await _httpClient.PostAsJsonAsync($"{uri}/Sss/AddSss", sssCreateDTO);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSss");
            }
            else
            {
                return RedirectToAction("GetAllSss");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSss(int id)
        {
            var result = await _httpClient.DeleteAsync($"{uri}/Sss/DeleteSss/{id}");
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSss");
            }
            else
            {
                return RedirectToAction("GetAllSss");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSss(int id)
        {
            var result = await _httpClient.GetAsync($"{uri}/Sss/GetSssById/{id}");
            if (result.IsSuccessStatusCode)
            {
                SssDTO response = await result.Content.ReadFromJsonAsync<SssDTO>();
                return View(response);


            }
            return RedirectToAction("GetAllSss");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSss(SssDTO sssDTO)
        {
            var result = await _httpClient.PutAsJsonAsync($"{uri}/Sss/UpdatedSss", sssDTO);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSss");
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSss()
        {
            var result = await _httpClient.GetAsync($"{uri}/Sss/GetAllSss");
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<IEnumerable<SssDTO>>();
                return View(response);
            }
            return NoContent();
        }



    }
}
