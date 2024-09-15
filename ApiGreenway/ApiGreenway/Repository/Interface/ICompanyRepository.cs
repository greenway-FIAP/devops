using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetCompanies();
    Task<Company> GetCompanyById(int CompanyId);
    Task<Company> AddCompany(Company Company);
    Task<Company> UpdateCompany(Company Company);
    void DeleteCompany(int CompanyId);
}
