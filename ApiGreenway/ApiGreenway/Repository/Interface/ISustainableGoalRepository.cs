using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface ISustainableGoalRepository
{
    Task<IEnumerable<SustainableGoal>> GetSustainableGoals();
    Task<SustainableGoal> GetSustainableGoalById(int SustainableGoalId);
    Task<SustainableGoal> AddSustainableGoal(SustainableGoal sustainableGoal);
    Task<SustainableGoal> UpdateSustainableGoal(SustainableGoal sustainableGoal);
    void DeleteSustainableGoal(int SustainableGoalId);
}
