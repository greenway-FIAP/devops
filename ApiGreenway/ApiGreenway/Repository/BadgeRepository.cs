using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class BadgeRepository : IBadgeRepository
{
    private readonly dbContext _dbContext;

    public BadgeRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Badge>> GetBadges()
    {
       return await _dbContext.Badges.Where(b => b.dt_finished_at == null).ToListAsync();
    }

    public async Task<Badge> GetBadgeById(int BadgeId)
    {
        return await _dbContext.Badges.FirstOrDefaultAsync(b => b.id_badge == BadgeId && b.dt_finished_at == null);
    }

    public async Task<Badge> AddBadge(Badge Badge)
    {
        var BadgeDb = await _dbContext.Badges.AddAsync(Badge);
        await _dbContext.SaveChangesAsync();
        return BadgeDb.Entity;
    }

    public async Task<Badge> UpdateBadge(Badge Badge)
    {
        var BadgeDb = await _dbContext.Badges.FirstOrDefaultAsync(b => b.id_badge == Badge.id_badge);
        if (BadgeDb == null)
        {
            return null; // Retorna null se o Badge não for encontrado
        }

        BadgeDb.ds_name = Badge.ds_name;
        BadgeDb.tx_description = Badge.tx_description;
        BadgeDb.ds_criteria = Badge.ds_criteria;
        BadgeDb.st_badge = Badge.st_badge;
        BadgeDb.url_image = Badge.url_image;
        BadgeDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        BadgeDb.id_badge_level = Badge.id_badge_level;
        BadgeDb.id_sustainable_goal = Badge.id_sustainable_goal;

        await _dbContext.SaveChangesAsync();
        return BadgeDb;
    }

    public async void DeleteBadge(int BadgeId)
    {
        var BadgeDb = await _dbContext.Badges.FirstOrDefaultAsync(b => b.id_badge == BadgeId);
        if (BadgeDb != null)
        {
            BadgeDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
