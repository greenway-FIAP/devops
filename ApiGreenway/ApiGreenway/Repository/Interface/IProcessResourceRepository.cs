using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IProcessResourceRepository
{
    Task<IEnumerable<ProcessResource>> GetProcessResources();
    Task<ProcessResource> GetProcessResourceById(int ProcessResourceId);
    Task<ProcessResource> AddProcessResource(ProcessResource processResource);
    Task<ProcessResource> UpdateProcessResource(ProcessResource processResource);
    void DeleteProcessResource(int ProcessResourceId);
}
