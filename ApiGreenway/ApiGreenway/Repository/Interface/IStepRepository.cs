using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IStepRepository
{
    Task<IEnumerable<Step>> GetSteps();
    Task<Step> GetStepById(int StepId);
    Task<Step> AddStep(Step step);
    Task<Step> UpdateStep(Step step);
    void DeleteStep(int StepId);
}
