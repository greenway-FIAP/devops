using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IProcessStepRepository
{
    Task<IEnumerable<ProcessStep>> GetProcessSteps();
    Task<ProcessStep> GetProcessStepById(int ProcessStepId);
    Task<ProcessStep> AddProcessStep(ProcessStep processStep);
    Task<ProcessStep> UpdateProcessStep(ProcessStep processStep);
    void DeleteProcessStep(int ProcessStepId);
}
