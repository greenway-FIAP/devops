using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class SectorRepository : ISectorRepository
{
    private readonly dbContext _dbContext;

    public SectorRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Sector>> GetSectors()
    {
        return await _dbContext.Sectors.Where(s => s.dt_finished_at == null).ToListAsync();
    }

    public async Task<Sector> GetSectorById(int SectorId)
    {
        return await _dbContext.Sectors.FirstOrDefaultAsync(s => s.id_sector == SectorId && s.dt_finished_at == null);
    }

    public async Task<Sector> AddSector(Sector sector)
    {
        var sectorDb = await _dbContext.Sectors.AddAsync(sector);
        await _dbContext.SaveChangesAsync();
        return sectorDb.Entity;
    }

    public async Task<Sector> UpdateSector(Sector sector)
    {
        var sectorDb = await _dbContext.Sectors.FirstOrDefaultAsync(s => s.id_sector == sector.id_sector);
        if (sectorDb == null)
        {
            return null; // Retorna null se o Sector não for encontrado
        }

        sectorDb.ds_name = sector.ds_name;
        sectorDb.tx_description = sector.tx_description;
        sectorDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

        await _dbContext.SaveChangesAsync();
        return sectorDb;
    }

    public async void DeleteSector(int SectorId)
    {
        var sectorDb = await _dbContext.Sectors.FirstOrDefaultAsync(s => s.id_sector == SectorId);
        if (sectorDb != null)
        {
            sectorDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
