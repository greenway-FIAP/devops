using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ResourceRepository : IResourceRepository
{
    private readonly dbContext _dbContext;

    public ResourceRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Resource>> GetResources()
    {
        return await _dbContext.Resources.Where(r => r.dt_finished_at == null).ToListAsync();
    }

    public async Task<Resource> GetResourceById(int ResourceId)
    {
        return await _dbContext.Resources.FirstOrDefaultAsync(r => r.id_resource == ResourceId && r.dt_finished_at == null);
    }

    public async Task<Resource> AddResource(Resource resource)
    {
        var resourceDb = await _dbContext.Resources.AddAsync(resource);
        await _dbContext.SaveChangesAsync();
        return resourceDb.Entity;
    }

    public async Task<Resource> UpdateResource(Resource resource)
    {
        var resourceDb = await _dbContext.Resources.FirstOrDefaultAsync(r => r.id_resource == resource.id_resource);
        if (resourceDb == null)
        {
            return null; // Retorna null se o Resource não for encontrado
        }

        resourceDb.ds_name = resource.ds_name;
        resourceDb.tx_description = resource.tx_description;
        resourceDb.vl_cost_per_unity = resource.vl_cost_per_unity;
        resourceDb.ds_unit_of_measurement = resource.ds_unit_of_measurement;
        resourceDb.nr_availability = resource.nr_availability;
        resourceDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        resourceDb.id_resource_type = resource.id_resource_type;

        await _dbContext.SaveChangesAsync();
        return resourceDb;
    }

    public async void DeleteResource(int ResourceId)
    {
        var resourceDb = await _dbContext.Resources.FirstOrDefaultAsync(r => r.id_resource == ResourceId);
        if (resourceDb != null)
        {
            resourceDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
