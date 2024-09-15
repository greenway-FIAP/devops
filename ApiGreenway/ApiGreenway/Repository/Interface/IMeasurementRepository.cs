using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IMeasurementRepository
{
    Task<IEnumerable<Measurement>> GetMeasurements();
    Task<Measurement> GetMeasurementById(int MeasurementId);
    Task<Measurement> AddMeasurement(Measurement Measurement);
    Task<Measurement> UpdateMeasurement(Measurement Measurement);
    void DeleteMeasurement(int MeasurementId);
}
