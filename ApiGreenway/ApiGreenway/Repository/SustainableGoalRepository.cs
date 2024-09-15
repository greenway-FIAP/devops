using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class SustainableGoalRepository : ISustainableGoalRepository
{
    private readonly dbContext _dbContext;

    public SustainableGoalRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<SustainableGoal>> GetSustainableGoals()
    {
        return await _dbContext.SustainableGoals.Where(s => s.dt_finished_at == null).ToListAsync();
    }

    public async Task<SustainableGoal> GetSustainableGoalById(int SustainableGoalId)
    {
        return await _dbContext.SustainableGoals.FirstOrDefaultAsync(s => s.id_sustainable_goal == SustainableGoalId && s.dt_finished_at == null);
    }

    public async Task<SustainableGoal> AddSustainableGoal(SustainableGoal sustainableGoal)
    {
        var sustainableGoalDb = await _dbContext.SustainableGoals.AddAsync(sustainableGoal);
        await _dbContext.SaveChangesAsync();
        return sustainableGoalDb.Entity;
    }

    public async Task<SustainableGoal> UpdateSustainableGoal(SustainableGoal sustainableGoal)
    {
        var sustainableGoalDb = await _dbContext.SustainableGoals.FirstOrDefaultAsync(s => s.id_sustainable_goal == sustainableGoal.id_sustainable_goal);
        if (sustainableGoalDb == null)
        {
            return null; // Retorna null se o SustainableGoal não for encontrado
        }

        sustainableGoalDb.ds_name = sustainableGoal.ds_name;
        sustainableGoalDb.tx_description = sustainableGoal.tx_description;
        sustainableGoalDb.vl_target = sustainableGoal.vl_target;
        sustainableGoalDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        sustainableGoalDb.id_badge = sustainableGoal.id_badge;

        await _dbContext.SaveChangesAsync();
        return sustainableGoalDb;
    }

    public async void DeleteSustainableGoal(int SustainableGoalId)
    {
        var sustainableGoalDb = await _dbContext.SustainableGoals.FirstOrDefaultAsync(s => s.id_sustainable_goal == SustainableGoalId);
        if (sustainableGoalDb != null)
        {
            sustainableGoalDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
