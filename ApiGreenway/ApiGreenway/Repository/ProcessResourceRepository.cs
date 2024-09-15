using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ProcessResourceRepository : IProcessResourceRepository
{
    private readonly dbContext _dbContext;

    public ProcessResourceRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<ProcessResource>> GetProcessResources()
    {
        return await _dbContext.ProcessResources.Where(p => p.dt_finished_at == null).ToListAsync();
    }

    public async Task<ProcessResource> GetProcessResourceById(int ProcessResourceId)
    {
        return await _dbContext.ProcessResources.FirstOrDefaultAsync(p => p.id_process_resource == ProcessResourceId && p.dt_finished_at == null);
    }

    public async Task<ProcessResource> AddProcessResource(ProcessResource processResource)
    {
        var processResourceDb = await _dbContext.ProcessResources.AddAsync(processResource);
        await _dbContext.SaveChangesAsync();
        return processResourceDb.Entity;
    }

    public async Task<ProcessResource> UpdateProcessResource(ProcessResource processResource)
    {
        var processResourceDb = await _dbContext.ProcessResources.FirstOrDefaultAsync(p => p.id_process_resource == processResource.id_process_resource);
        if (processResourceDb == null)
        {
            return null; // Retorna null se o ProcessResource não for encontrado
        }

        processResourceDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        processResourceDb.id_process = processResource.id_process;
        processResourceDb.id_resource = processResource.id_resource;

        await _dbContext.SaveChangesAsync();
        return processResourceDb;
    }
    public async void DeleteProcessResource(int ProcessResourceId)
    {
        var processResourceDb = await _dbContext.ProcessResources.FirstOrDefaultAsync(p => p.id_process_resource == ProcessResourceId);
        if (processResourceDb != null)
        {
            processResourceDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
