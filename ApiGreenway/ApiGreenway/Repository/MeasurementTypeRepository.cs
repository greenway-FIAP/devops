using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class MeasurementTypeRepository : IMeasurementTypeRepository
{
    private readonly dbContext _dbContext;

    public MeasurementTypeRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<MeasurementType>> GetMeasurementTypes()
    {
        return await _dbContext.MeasurementTypes.Where(m => m.dt_finished_at == null).ToListAsync();
    }

    public async Task<MeasurementType> GetMeasurementTypeById(int MeasurementTypeId)
    {
        return await _dbContext.MeasurementTypes.FirstOrDefaultAsync(m => m.id_measurement_type == MeasurementTypeId && m.dt_finished_at == null);
    }

    public async Task<MeasurementType> AddMeasurementType(MeasurementType measurementType)
    {
        var measurementTypeDb = await _dbContext.MeasurementTypes.AddAsync(measurementType);
        await _dbContext.SaveChangesAsync();
        return measurementTypeDb.Entity;
    }

    public async Task<MeasurementType> UpdateMeasurementType(MeasurementType measurementType)
    {
        var measurementTypeDb = await _dbContext.MeasurementTypes.FirstOrDefaultAsync(m => m.id_measurement_type == measurementType.id_measurement_type);
        if (measurementTypeDb == null)
        {
            return null; // Retorna null se o MeasurementType não for encontrado
        }

        measurementTypeDb.ds_name = measurementType.ds_name;
        measurementTypeDb.tx_description = measurementType.tx_description;
        measurementTypeDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

        await _dbContext.SaveChangesAsync();
        return measurementTypeDb;
    }

    public async void DeleteMeasurementType(int MeasurementTypeId)
    {
        var measurementTypeDb = await _dbContext.MeasurementTypes.FirstOrDefaultAsync(m => m.id_measurement_type == MeasurementTypeId);
        if (measurementTypeDb != null)
        {
            measurementTypeDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
