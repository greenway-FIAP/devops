using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IBadgeLevelRepository
{
    Task<IEnumerable<BadgeLevel>> GetBadgeLevels();
    Task<BadgeLevel> GetBadgeLevelById(int BadgeLevelId);
    Task<BadgeLevel> AddBadgeLevel(BadgeLevel BadgeLevel);
    Task<BadgeLevel> UpdateBadgeLevel(BadgeLevel BadgeLevel);
    void DeleteBadgeLevel(int BadgeLevelId);
}
