using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface ISustainableImprovementActionsRepository
{
    Task<IEnumerable<SustainableImprovementActions>> GetSustainableImprovementActions();
    Task<SustainableImprovementActions> GetSustainableImprovementActionsById(int SustainableImprovementActionsId);
    Task<SustainableImprovementActions> AddSustainableImprovementActions(SustainableImprovementActions sustainableImprovementActions);
    Task<SustainableImprovementActions> UpdateSustainableImprovementActions(SustainableImprovementActions sustainableImprovementActions);
    void DeleteSustainableImprovementActions(int SustainableImprovementActionsId);
}
