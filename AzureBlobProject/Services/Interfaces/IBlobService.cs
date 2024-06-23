namespace AzureBlobProject.Services.Interfaces
{
    public interface IBlobService
    {
        string GetBlobAsync(string blobName, string containerName);
        Task<List<string>> GetAllBlobsAsync(string containerName);
        Task<bool> CreateBlobAsync(string containerName, IFormFile blobFile, string blobName);
        Task DeleteBlobAsync(string blobName, string containerName);

    }
}
