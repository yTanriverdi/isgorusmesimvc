using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.AdminPanel.Models.DTOs.Log;

namespace MVC.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class LogController : Controller
    {
        private readonly HttpClient _httpClient;

        public LogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        string uri = "http://localhost:5211/api";

        [HttpGet]
        public async Task<IActionResult> DeleteLog(int logId)
        {
            var result = await _httpClient.DeleteAsync($"{uri}/Log/DeleteLog/{logId}");
            if (result.IsSuccessStatusCode)
            {
                return Ok(result);
            }
            return BadRequest("Silme işlemi başarısız");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllLogs");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> logs = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(logs);
            }
            return BadRequest(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetLogById(int logId)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetLogById/{logId}");
            if (result.IsSuccessStatusCode)
            {
                LogDTO log = await result.Content.ReadFromJsonAsync<LogDTO>();
                return View(log);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllErrorLogs()
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllErrorLogs");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> errorLogs = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return Ok(errorLogs);
            }
            return BadRequest(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllInformationLogs()
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllInformationLogs");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> informationLogs = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(informationLogs);
            }
            return BadRequest(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllErrorLogsByUserId(int userId)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllErrorLogsByUserId/{userId}");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> userErrorLogs = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(userErrorLogs);
            }
            return BadRequest(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllInformationLogsByUserId(int userId)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllInformationLogsByUserId/{userId}");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> userInformationLogs = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(userInformationLogs);
            }
            return BadRequest(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllLogsByDate(DateTime firstDate, DateTime secondDate)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllLogsByDate/{firstDate}/{secondDate}");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> logsByDate = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(logsByDate);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllErrorLogsByDate(DateTime firstDate, DateTime secondDate)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllErrorLogsByDate/{firstDate}/{secondDate}");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> errorByDate = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(errorByDate);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInformationLogsByDate(DateTime firstDate, DateTime secondDate)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllInformationLogsByDate/{firstDate}/{secondDate}");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> informationByDate = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(informationByDate);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInformationLogsByDateandUserId(DateTime firstDate, DateTime secondDate, int userId)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllInformationLogsByDateandUserId/{firstDate}/{secondDate}/{userId}");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> informationByDateandUser = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(informationByDateandUser);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllErrorLogsByDateandUserIdAsync(DateTime firstDate, DateTime secondDate, int userId)
        {
            var result = await _httpClient.GetAsync($"{uri}/Log/GetAllErrorLogsByDateandUserIdAsync/{firstDate}/{secondDate}/{userId}");
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<LogDTO> errorByDateandUser = await result.Content.ReadFromJsonAsync<IEnumerable<LogDTO>>();
                return View(errorByDateandUser);
            }
            return BadRequest(result);
        }
    }
}
