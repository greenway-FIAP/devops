using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IMeasurementTypeRepository
{
    Task<IEnumerable<MeasurementType>> GetMeasurementTypes();
    Task<MeasurementType> GetMeasurementTypeById(int MeasurementTypeId);
    Task<MeasurementType> AddMeasurementType(MeasurementType measurementType);
    Task<MeasurementType> UpdateMeasurementType(MeasurementType measurementType);
    void DeleteMeasurementType(int MeasurementTypeId);
}
