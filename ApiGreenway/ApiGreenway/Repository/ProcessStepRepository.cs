using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ProcessStepRepository : IProcessStepRepository
{
    private readonly dbContext _dbContext;

    public ProcessStepRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<ProcessStep>> GetProcessSteps()
    {
        return await _dbContext.ProcessSteps.Where(p => p.dt_finished_at == null).ToListAsync();
    }

    public async Task<ProcessStep> GetProcessStepById(int ProcessStepId)
    {
        return await _dbContext.ProcessSteps.FirstOrDefaultAsync(p => p.id_process_step == ProcessStepId && p.dt_finished_at == null);
    }

    public async Task<ProcessStep> AddProcessStep(ProcessStep processStep)
    {
        var processStepDb = await _dbContext.ProcessSteps.AddAsync(processStep);
        await _dbContext.SaveChangesAsync();
        return processStepDb.Entity;
    }

    public async Task<ProcessStep> UpdateProcessStep(ProcessStep processStep)
    {
        var processStepDb = await _dbContext.ProcessSteps.FirstOrDefaultAsync(p => p.id_process_step == processStep.id_process_step);
        if (processStepDb == null)
        {
            return null; // Retorna null se o ProcessStep não for encontrado
        }

        processStepDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        processStepDb.id_step = processStep.id_step;
        processStepDb.id_process = processStep.id_process;

        await _dbContext.SaveChangesAsync();
        return processStepDb;
    }

    public async void DeleteProcessStep(int ProcessStepId)
    {
        var processStepDb = await _dbContext.ProcessSteps.FirstOrDefaultAsync(p => p.id_process_step == ProcessStepId);
        if (processStepDb != null)
        {
            processStepDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
