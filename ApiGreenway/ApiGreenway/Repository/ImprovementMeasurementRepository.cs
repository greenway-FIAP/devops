using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ImprovementMeasurementRepository : IImprovementMeasurementRepository
{
    private readonly dbContext _dbContext;

    public ImprovementMeasurementRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<ImprovementMeasurement>> GetImprovementMeasurements()
    {
        return await _dbContext.ImprovementMeasurements.Where(i => i.dt_finished_at == null).ToListAsync();
    }

    public async Task<ImprovementMeasurement> GetImprovementMeasurementById(int ImprovementMeasurementId)
    {
        return await _dbContext.ImprovementMeasurements.FirstOrDefaultAsync(i => i.id_improvement_measurement == ImprovementMeasurementId && i.dt_finished_at == null);
    }

    public async Task<ImprovementMeasurement> AddImprovementMeasurement(ImprovementMeasurement ImprovementMeasurement)
    {
        var ImprovementMeasurementDb = await _dbContext.ImprovementMeasurements.AddAsync(ImprovementMeasurement);
        await _dbContext.SaveChangesAsync();
        return ImprovementMeasurementDb.Entity;
    }

    public async Task<ImprovementMeasurement> UpdateImprovementMeasurement(ImprovementMeasurement ImprovementMeasurement)
    {
        var ImprovementMeasurementDb = await _dbContext.ImprovementMeasurements.FirstOrDefaultAsync(i => i.id_improvement_measurement == ImprovementMeasurement.id_improvement_measurement);
        if (ImprovementMeasurementDb == null)
        {
            return null; // Retorna null se o ImprovementMeasurement não for encontrado
        }

        ImprovementMeasurementDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        ImprovementMeasurementDb.id_sustainable_improvement_actions = ImprovementMeasurement.id_sustainable_improvement_actions;


        await _dbContext.SaveChangesAsync();
        return ImprovementMeasurementDb;
    }

    public async void DeleteImprovementMeasurement(int ImprovementMeasurementId)
    {
        var ImprovementMeasurementDb = await _dbContext.ImprovementMeasurements.FirstOrDefaultAsync(i => i.id_improvement_measurement == ImprovementMeasurementId);
        if (ImprovementMeasurementDb != null)
        {
            ImprovementMeasurementDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
