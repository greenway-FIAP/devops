using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class CompanyRepresentativeRepository : ICompanyRepresentativeRepository
{
    private readonly dbContext _dbContext;

    public CompanyRepresentativeRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<CompanyRepresentative>> GetCompanyRepresentatives()
    {
        return await _dbContext.CompanyRepresentatives.Where(c => c.dt_finished_at == null).ToListAsync();
    }

    public async Task<CompanyRepresentative> GetCompanyRepresentativeById(int CompanyRepresentativeId)
    {
        return await _dbContext.CompanyRepresentatives.FirstOrDefaultAsync(c => c.id_company_representative == CompanyRepresentativeId && c.dt_finished_at == null);
    }

    public async Task<CompanyRepresentative> AddCompanyRepresentative(CompanyRepresentative CompanyRepresentative)
    {
        var CompanyRepresentativeDb = await _dbContext.CompanyRepresentatives.AddAsync(CompanyRepresentative);
        await _dbContext.SaveChangesAsync();
        return CompanyRepresentativeDb.Entity;
    }

    public async Task<CompanyRepresentative> UpdateCompanyRepresentative(CompanyRepresentative CompanyRepresentative)
    {
        var CompanyRepresentativeDb = await _dbContext.CompanyRepresentatives.FirstOrDefaultAsync(c => c.id_company_representative == CompanyRepresentative.id_company_representative);
        if (CompanyRepresentativeDb == null)
        {
            return null; // Retorna null se o CompanyRepresentative não for encontrado
        }

        CompanyRepresentativeDb.nr_phone = CompanyRepresentative.nr_phone;
        CompanyRepresentativeDb.ds_role = CompanyRepresentative.ds_role;
        CompanyRepresentativeDb.ds_name = CompanyRepresentative.ds_name;
        CompanyRepresentativeDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        CompanyRepresentativeDb.id_user = CompanyRepresentative.id_user;
        CompanyRepresentativeDb.id_company = CompanyRepresentative.id_company;

        await _dbContext.SaveChangesAsync();
        return CompanyRepresentativeDb;
    }

    public async void DeleteCompanyRepresentative(int CompanyRepresentativeId)
    {
        var CompanyRepresentativeDb = await _dbContext.CompanyRepresentatives.FirstOrDefaultAsync(c => c.id_company_representative == CompanyRepresentativeId);
        if (CompanyRepresentativeDb != null)
        {
            CompanyRepresentativeDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
