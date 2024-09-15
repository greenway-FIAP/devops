using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class StepRepository : IStepRepository
{
    private readonly dbContext _dbContext;

    public StepRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Step>> GetSteps()
    {
        return await _dbContext.Steps.Where(s => s.dt_finished_at == null).ToListAsync();
    }

    public async Task<Step> GetStepById(int StepId)
    {
        return await _dbContext.Steps.FirstOrDefaultAsync(s => s.id_step == StepId && s.dt_finished_at == null);
    }


    public async Task<Step> AddStep(Step step)
    {
        var stepDb = await _dbContext.Steps.AddAsync(step);
        await _dbContext.SaveChangesAsync();
        return stepDb.Entity;
    }

    public async Task<Step> UpdateStep(Step step)
    {
        var stepDb = await _dbContext.Steps.FirstOrDefaultAsync(s => s.id_step == step.id_step);
        if (stepDb == null)
        {
            return null; // Retorna null se o Step não for encontrado
        }

        stepDb.ds_name = step.ds_name;
        stepDb.tx_description = step.tx_description;
        stepDb.nr_estimated_time = step.nr_estimated_time;
        stepDb.st_step = step.st_step;
        stepDb.dt_start = step.dt_start;
        stepDb.dt_end = step.dt_end;
        stepDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

        await _dbContext.SaveChangesAsync();
        return stepDb;
    }

    public async void DeleteStep(int StepId)
    {
        var stepDb = await _dbContext.Steps.FirstOrDefaultAsync(s => s.id_step == StepId);
        if (stepDb != null)
        {
            stepDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
