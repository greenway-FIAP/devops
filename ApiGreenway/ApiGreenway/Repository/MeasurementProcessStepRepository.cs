using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class MeasurementProcessStepRepository : IMeasurementProcessStepRepository
{
    private readonly dbContext _dbContext;

    public MeasurementProcessStepRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<MeasurementProcessStep>> GetMeasurementProcessSteps()
    {
        return await _dbContext.MeasurementProcessSteps.Where(m => m.dt_finished_at == null).ToListAsync();
    }

    public async Task<MeasurementProcessStep> GetMeasurementProcessStepById(int MeasurementProcessStepId)
    {
        return await _dbContext.MeasurementProcessSteps.FirstOrDefaultAsync(m => m.id_measurement_process_step == MeasurementProcessStepId && m.dt_finished_at == null);
    }

    public async Task<MeasurementProcessStep> AddMeasurementProcessStep(MeasurementProcessStep MeasurementProcessStep)
    {
        var MeasurementProcessStepDb = await _dbContext.MeasurementProcessSteps.AddAsync(MeasurementProcessStep);
        await _dbContext.SaveChangesAsync();
        return MeasurementProcessStepDb.Entity;
    }

    public async Task<MeasurementProcessStep> UpdateMeasurementProcessStep(MeasurementProcessStep MeasurementProcessStep)
    {
        var MeasurementProcessStepDb = await _dbContext.MeasurementProcessSteps.FirstOrDefaultAsync(m => m.id_measurement_process_step == MeasurementProcessStep.id_measurement_process_step);
        if (MeasurementProcessStepDb == null)
        {
            return null; // Retorna null se o MeasurementProcessStep não for encontrado
        }

        MeasurementProcessStepDb.nr_result = MeasurementProcessStep.nr_result;
        MeasurementProcessStepDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        MeasurementProcessStepDb.id_measurement = MeasurementProcessStep.id_measurement;
        MeasurementProcessStepDb.id_process_step = MeasurementProcessStep.id_process_step;

        await _dbContext.SaveChangesAsync();
        return MeasurementProcessStepDb;
    }

    public async void DeleteMeasurementProcessStep(int MeasurementProcessStepId)
    {
        var MeasurementProcessStepDb = await _dbContext.MeasurementProcessSteps.FirstOrDefaultAsync(m => m.id_measurement_process_step == MeasurementProcessStepId);
        if (MeasurementProcessStepDb != null)
        {
            MeasurementProcessStepDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
