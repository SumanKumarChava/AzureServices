using AzureBlobProject.Models;
using AzureBlobProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureBlobProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContainerService _containerService;
        private IBlobService _blobService;

        public HomeController(ILogger<HomeController> logger, IContainerService containerService, IBlobService blobService)
        {
            _logger = logger;
            this._containerService = containerService;
            _blobService = blobService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _containerService.GetContainerAndBlobs();
            return View(list);
        }

        public async Task<IActionResult> Images()
        {
            return View(await _blobService.GetAllBlobsAlongWithDetailsAsync("private-container"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
