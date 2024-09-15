using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IResourceTypeRepository
{
    Task<IEnumerable<ResourceType>> GetResourceTypes();
    Task<ResourceType> GetResourceTypeById(int ResourceTypeId);
    Task<ResourceType> AddResourceType(ResourceType resourceType);
    Task<ResourceType> UpdateResourceType(ResourceType resourceType);
    void DeleteResourceType(int ResourceTypeId);
}
