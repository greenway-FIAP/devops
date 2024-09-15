using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IMeasurementProcessStepRepository
{
    Task<IEnumerable<MeasurementProcessStep>> GetMeasurementProcessSteps();
    Task<MeasurementProcessStep> GetMeasurementProcessStepById(int MeasurementProcessStepId);
    Task<MeasurementProcessStep> AddMeasurementProcessStep(MeasurementProcessStep MeasurementProcessStep);
    Task<MeasurementProcessStep> UpdateMeasurementProcessStep(MeasurementProcessStep MeasurementProcessStep);
    void DeleteMeasurementProcessStep(int MeasurementProcessStepId);
}
