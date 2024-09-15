using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IResourceRepository
{
    Task<IEnumerable<Resource>> GetResources();
    Task<Resource> GetResourceById(int ResourceId);
    Task<Resource> AddResource(Resource resource);
    Task<Resource> UpdateResource(Resource resource);
    void DeleteResource(int ResourceId);
}
