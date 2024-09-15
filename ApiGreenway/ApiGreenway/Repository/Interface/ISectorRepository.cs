using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface ISectorRepository
{
    Task<IEnumerable<Sector>> GetSectors();
    Task<Sector> GetSectorById(int SectorId);
    Task<Sector> AddSector(Sector sector);
    Task<Sector> UpdateSector(Sector sector);
    void DeleteSector(int SectorId);
}
