using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly dbContext _dbContext;

    public MeasurementRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Measurement>> GetMeasurements()
    {
        return await _dbContext.Measurements.Where(m => m.dt_finished_at == null).ToListAsync();
    }

    public async Task<Measurement> GetMeasurementById(int MeasurementId)
    {
        return await _dbContext.Measurements.FirstOrDefaultAsync(m => m.id_measurement == MeasurementId && m.dt_finished_at == null);
    }

    public async Task<Measurement> AddMeasurement(Measurement Measurement)
    {
        var MeasurementDb = await _dbContext.Measurements.AddAsync(Measurement);
        await _dbContext.SaveChangesAsync();
        return MeasurementDb.Entity;
    }

    public async Task<Measurement> UpdateMeasurement(Measurement Measurement)
    {
        var MeasurementDb = await _dbContext.Measurements.FirstOrDefaultAsync(m => m.id_measurement == Measurement.id_measurement);
        if (MeasurementDb == null)
        {
            return null; // Retorna null se o Measurement não for encontrado
        }

        MeasurementDb.ds_name = Measurement.ds_name;
        MeasurementDb.tx_description = Measurement.tx_description;
        MeasurementDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        MeasurementDb.id_measurement_type = Measurement.id_measurement_type;
        MeasurementDb.id_improvement_measurement = Measurement.id_improvement_measurement;
        MeasurementDb.id_sustainable_goal = Measurement.id_sustainable_goal;

        await _dbContext.SaveChangesAsync();
        return MeasurementDb;
    }

    public async void DeleteMeasurement(int MeasurementId)
    {
        var MeasurementDb = await _dbContext.Measurements.FirstOrDefaultAsync(m => m.id_measurement == MeasurementId);
        if (MeasurementDb != null)
        {
            MeasurementDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
