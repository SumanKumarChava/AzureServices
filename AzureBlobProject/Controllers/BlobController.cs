using AzureBlobProject.Models;
using AzureBlobProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers
{
    public class BlobController : Controller
    {
        private IBlobService _blobService;
        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<IActionResult> Manage(string containerName)
        {
            var obj = await _blobService.GetAllBlobsAsync(containerName);
            return View(obj);
        }

        public IActionResult AddFile(string containerName)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file, string containerName, Blob blob)
        {
            if(file == null || file.Length < 1)
            {
                return View();
            }
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            var result = await _blobService.CreateBlobAsync(containerName, file, fileName, blob);
            if(result)
                return RedirectToAction("Index", "Container");
            return View();
        }

        public IActionResult ViewFile(string containerName, string name)
        {
            var blob = _blobService.GetBlobAsync(name, containerName);
            return Redirect(blob);
        }

        public async Task<IActionResult> DeleteFile(string containerName, string name)
        {
            await _blobService.DeleteBlobAsync(name, containerName);
            return RedirectToAction("Index", "Container");
        }

    }
}
