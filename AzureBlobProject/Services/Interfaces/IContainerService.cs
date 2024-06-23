namespace AzureBlobProject.Services.Interfaces
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainers();
        Task<List<string>> GetContainerAndBlobs();
        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);
    }
}
