using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class SustainableImprovementActionsRepository : ISustainableImprovementActionsRepository
{
    private readonly dbContext _dbContext;

    public SustainableImprovementActionsRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<SustainableImprovementActions>> GetSustainableImprovementActions()
    {
        return await _dbContext.SustainableImprovementActions.Where(s => s.dt_finished_at == null).ToListAsync();
    }

    public async Task<SustainableImprovementActions> GetSustainableImprovementActionsById(int SustainableImprovementActionsId)
    {
        return await _dbContext.SustainableImprovementActions.FirstOrDefaultAsync(s => s.id_sustainable_improvement_action == SustainableImprovementActionsId && s.dt_finished_at == null);
    }

    public async Task<SustainableImprovementActions> AddSustainableImprovementActions(SustainableImprovementActions sustainableImprovementActions)
    {
        var sustainableImprovementActionsDb = await _dbContext.SustainableImprovementActions.AddAsync(sustainableImprovementActions);
        await _dbContext.SaveChangesAsync();
        return sustainableImprovementActionsDb.Entity;
    }

    public async Task<SustainableImprovementActions> UpdateSustainableImprovementActions(SustainableImprovementActions sustainableImprovementActions)
    {
        var sustainableImprovementActionsDb = await _dbContext.SustainableImprovementActions.FirstOrDefaultAsync(s => s.id_sustainable_improvement_action == sustainableImprovementActions.id_sustainable_improvement_action);
        if (sustainableImprovementActionsDb == null)
        {
            return null; // Retorna null se o SustainableImprovementActions não for encontrado
        }

        sustainableImprovementActionsDb.ds_name = sustainableImprovementActions.ds_name;
        sustainableImprovementActionsDb.tx_instruction = sustainableImprovementActions.tx_instruction;
        sustainableImprovementActionsDb.st_sustainable_action = sustainableImprovementActions.st_sustainable_action;
        sustainableImprovementActionsDb.nr_priority = sustainableImprovementActions.nr_priority;
        sustainableImprovementActionsDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        sustainableImprovementActionsDb.id_sustainable_goal = sustainableImprovementActions.id_sustainable_goal;

        await _dbContext.SaveChangesAsync();
        return sustainableImprovementActionsDb;
    }

    public async void DeleteSustainableImprovementActions(int SustainableImprovementActionsId)
    {
        var sustainableImprovementActionsDb = await _dbContext.SustainableImprovementActions.FirstOrDefaultAsync(s => s.id_sustainable_improvement_action == SustainableImprovementActionsId);
        if (sustainableImprovementActionsDb != null)
        {
            sustainableImprovementActionsDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
