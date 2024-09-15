using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IBadgeRepository
{
    Task<IEnumerable<Badge>> GetBadges();
    Task<Badge> GetBadgeById(int BadgeId);
    Task<Badge> AddBadge(Badge Badge);
    Task<Badge> UpdateBadge(Badge Badge);
    void DeleteBadge(int BadgeId);
}
