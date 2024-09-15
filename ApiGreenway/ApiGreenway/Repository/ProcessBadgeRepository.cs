using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ProcessBadgeRepository : IProcessBadgeRepository
{
    private readonly dbContext _dbContext;

    public ProcessBadgeRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<ProcessBadge>> GetProcessBadges()
    {
        return await _dbContext.ProcessBadges.Where(p => p.dt_finished_at == null).ToListAsync();
    }
    public async Task<ProcessBadge> GetProcessBadgeById(int ProcessBadgeId)
    {
        return await _dbContext.ProcessBadges.FirstOrDefaultAsync(p => p.id_process_badge == ProcessBadgeId && p.dt_finished_at == null);
    }

    public async Task<ProcessBadge> AddProcessBadge(ProcessBadge processBadge)
    {
        var processBadgeDb = await _dbContext.ProcessBadges.AddAsync(processBadge);
        await _dbContext.SaveChangesAsync();
        return processBadgeDb.Entity;
    }

    public async Task<ProcessBadge> UpdateProcessBadge(ProcessBadge processBadge)
    {
        var processBadgeDb = await _dbContext.ProcessBadges.FirstOrDefaultAsync(p => p.id_process_badge == processBadge.id_process_badge);
        if (processBadgeDb == null)
        {
            return null; // Retorna null se o ProcessBadge não for encontrado
        }

        processBadgeDb.dt_expiration_date = processBadge.dt_expiration_date;
        processBadgeDb.url_badge = processBadge.url_badge;
        processBadgeDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        processBadgeDb.id_process = processBadge.id_process;
        processBadgeDb.id_badge = processBadge.id_badge;

        await _dbContext.SaveChangesAsync();
        return processBadgeDb;
    }

    public async void DeleteProcessBadge(int ProcessBadgeId)
    {
        var processBadgeDb = await _dbContext.ProcessBadges.FirstOrDefaultAsync(p => p.id_process_badge == ProcessBadgeId);
        if (processBadgeDb != null)
        {
            processBadgeDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
