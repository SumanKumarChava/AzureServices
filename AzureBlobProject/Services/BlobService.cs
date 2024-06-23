using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobProject.Services.Interfaces;
using System.Net.Http.Headers;

namespace AzureBlobProject.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient? _containerClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<bool> CreateBlobAsync(string containerName, IFormFile blobFile, string blobName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = _containerClient.GetBlobClient(blobName);
            var httpHeaders = new BlobHttpHeaders
            {
                ContentType = blobFile.ContentType
            };

            var result = await blobClient.UploadAsync(blobFile.OpenReadStream(), httpHeaders);
            if (result != null)
                return true;

            return false;
        }

        public async Task DeleteBlobAsync(string blobName, string containerName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllBlobsAsync(string containerName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var allBlobs = _containerClient.GetBlobsAsync();
            List<string> allBlobNames = new List<string>();
            await foreach (var item in allBlobs)
            {
                allBlobNames.Add(item.Name);
            }
            return allBlobNames;
        }

        public string GetBlobAsync(string blobName, string containerName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = _containerClient.GetBlobClient(blobName);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
