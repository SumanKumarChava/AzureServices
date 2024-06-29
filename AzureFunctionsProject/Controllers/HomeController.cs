using AzureFunctionsProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AzureFunctionsProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient httpClient = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new SalesRequest());
        }

        //http://localhost:7078/api/WriteToQueue
        [HttpPost]
        public async Task<IActionResult> Index(SalesRequest sr)
        {
            sr.Id = Guid.NewGuid().ToString();
            using(var content = new StringContent(JsonConvert.SerializeObject(sr), System.Text.Encoding.UTF8))
            {
                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:7078/api/WriteToQueue", content);
                string returnValue = response.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
