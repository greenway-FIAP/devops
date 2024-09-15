using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly dbContext _dbContext;

    public CompanyRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Company>> GetCompanies()
    {
        return await _dbContext.Companies.Where(c => c.dt_finished_at == null).ToListAsync();
    }

    public async Task<Company> GetCompanyById(int CompanyId)
    {
        return await _dbContext.Companies.FirstOrDefaultAsync(c => c.id_company == CompanyId && c.dt_finished_at == null);
    }

    public async Task<Company> AddCompany(Company Company)
    {
        var CompanyDb = await _dbContext.Companies.AddAsync(Company);
        await _dbContext.SaveChangesAsync();
        return CompanyDb.Entity;
    }

    public async Task<Company> UpdateCompany(Company Company)
    {
        var CompanyDb = await _dbContext.Companies.FirstOrDefaultAsync(c => c.id_company == Company.id_company);
        if (CompanyDb == null)
        {
            return null; // Retorna null se o Company não for encontrado
        }

        CompanyDb.ds_name = Company.ds_name;
        CompanyDb.tx_description = Company.tx_description;
        CompanyDb.vl_current_revenue = Company.vl_current_revenue;
        CompanyDb.nr_size = Company.nr_size;
        CompanyDb.nr_cnpj = Company.nr_cnpj;
        CompanyDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        CompanyDb.id_sector = Company.id_sector;
        CompanyDb.id_address = Company.id_address;

        await _dbContext.SaveChangesAsync();
        return CompanyDb;
    }

    public async void DeleteCompany(int CompanyId)
    {
        var CompanyDb = await _dbContext.Companies.FirstOrDefaultAsync(c => c.id_company == CompanyId);
        if (CompanyDb != null)
        {
            CompanyDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
