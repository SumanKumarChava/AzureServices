
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobProject.Services.Interfaces;

namespace AzureBlobProject.Services
{
    public class ContainerService : IContainerService
    {
        private BlobServiceClient _blobServiceClient;
        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            
        }
        public async Task CreateContainer(string containerName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteContainer(string containerName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainers()
        {
            var containers = new List<string>();
            await foreach(BlobContainerItem item in _blobServiceClient.GetBlobContainersAsync())
            {
                containers.Add(item.Name);
            }
            return containers;
        }

        public Task<List<string>> GetContainerAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
