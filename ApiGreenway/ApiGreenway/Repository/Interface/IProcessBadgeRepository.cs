using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IProcessBadgeRepository
{
    Task<IEnumerable<ProcessBadge>> GetProcessBadges();
    Task<ProcessBadge> GetProcessBadgeById(int ProcessBadgeId);
    Task<ProcessBadge> AddProcessBadge(ProcessBadge processBadge);
    Task<ProcessBadge> UpdateProcessBadge(ProcessBadge processBadge);
    void DeleteProcessBadge(int ProcessBadgeId);
}
