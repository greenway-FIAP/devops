using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface ICompanyRepresentativeRepository
{
    Task<IEnumerable<CompanyRepresentative>> GetCompanyRepresentatives();
    Task<CompanyRepresentative> GetCompanyRepresentativeById(int CompanyRepresentativeId);
    Task<CompanyRepresentative> AddCompanyRepresentative(CompanyRepresentative CompanyRepresentative);
    Task<CompanyRepresentative> UpdateCompanyRepresentative(CompanyRepresentative CompanyRepresentative);
    void DeleteCompanyRepresentative(int CompanyRepresentativeId);
}
