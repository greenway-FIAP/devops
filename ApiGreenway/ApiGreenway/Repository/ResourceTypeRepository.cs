using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ResourceTypeRepository : IResourceTypeRepository
{
    private readonly dbContext _dbContext;

    public ResourceTypeRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<ResourceType>> GetResourceTypes()
    {
        return await _dbContext.ResourceTypes.Where(r => r.dt_finished_at == null).ToListAsync();
    }

    public async Task<ResourceType> GetResourceTypeById(int ResourceTypeId)
    {
        return await _dbContext.ResourceTypes.FirstOrDefaultAsync(r => r.id_resource_type == ResourceTypeId && r.dt_finished_at == null);
    }

    public async Task<ResourceType> AddResourceType(ResourceType resourceType)
    {
        var resourceTypeDb = await _dbContext.ResourceTypes.AddAsync(resourceType);
        await _dbContext.SaveChangesAsync();
        return resourceTypeDb.Entity;
    }

    public async Task<ResourceType> UpdateResourceType(ResourceType resourceType)
    {
        var resourceTypeDb = await _dbContext.ResourceTypes.FirstOrDefaultAsync(r => r.id_resource_type == resourceType.id_resource_type);
        if (resourceTypeDb == null)
        {
            return null; // Retorna null se o ResourceType não for encontrado
        }

        resourceTypeDb.ds_name = resourceType.ds_name;
        resourceTypeDb.tx_description = resourceType.tx_description;
        resourceTypeDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

        await _dbContext.SaveChangesAsync();
        return resourceTypeDb;
    }

    public async void DeleteResourceType(int ResourceTypeId)
    {
        var resourceTypeDb = await _dbContext.ResourceTypes.FirstOrDefaultAsync(r => r.id_resource_type == ResourceTypeId);
        if (resourceTypeDb != null)
        {
            resourceTypeDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
