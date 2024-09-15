using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IImprovementMeasurementRepository
{
    Task<IEnumerable<ImprovementMeasurement>> GetImprovementMeasurements();
    Task<ImprovementMeasurement> GetImprovementMeasurementById(int ImprovementMeasurementId);
    Task<ImprovementMeasurement> AddImprovementMeasurement(ImprovementMeasurement ImprovementMeasurement);
    Task<ImprovementMeasurement> UpdateImprovementMeasurement(ImprovementMeasurement ImprovementMeasurement);
    void DeleteImprovementMeasurement(int ImprovementMeasurementId);
}
