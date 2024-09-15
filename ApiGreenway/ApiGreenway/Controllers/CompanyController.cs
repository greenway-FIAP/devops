using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/company")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        this._companyRepository = companyRepository;
    }

    /// <summary>
    /// Obtém todas as empresas.
    /// </summary>
    /// <returns>Uma lista de empresas.</returns>
    /// <response code="200">Retorna a lista de empresas</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
    {
        try
        {
            var companies = await _companyRepository.GetCompanies();
            return Ok(companies);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém uma empresa pelo ID.
    /// </summary>
    /// <param name="companyId">O ID da empresa.</param>
    /// <returns>Uma empresa específica.</returns>
    /// <response code="200">Retorna a empresa com o ID fornecido</response>
    /// <response code="404">Se a empresa não for encontrada</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{companyId:int}")]
    public async Task<ActionResult<Company>> GetCompanyById(int companyId)
    {
        try
        {
            var company = await _companyRepository.GetCompanyById(companyId);
            if (company == null)
            {
                return NotFound($"Empresa com o Id: {companyId}, não foi encontrada");
            }

            return Ok(company);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria uma nova empresa.
    /// </summary>
    /// <param name="company">Os dados da empresa a ser criada.</param>
    /// <returns>A empresa criada.</returns>
    /// <response code="201">Retorna a empresa criada</response>
    /// <response code="400">Se os dados da empresa forem inválidos</response>
    /// <response code="500">Erro ao salvar os dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<Company>> CreateCompany([FromBody] Company company)
    {
        try
        {
            if (company == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdCompany = await _companyRepository.AddCompany(company);
            return CreatedAtAction(nameof(GetCompanyById), new
            {
                companyId = createdCompany.id_company
            }, createdCompany);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza uma empresa existente.
    /// </summary>
    /// <param name="companyId">O ID da empresa a ser atualizada.</param>
    /// <param name="company">Os novos dados da empresa.</param>
    /// <returns>A empresa atualizada.</returns>
    /// <response code="200">Retorna a empresa atualizada</response>
    /// <response code="404">Se a empresa não for encontrada</response>
    /// <response code="400">Se os dados da empresa forem inválidos</response>
    /// <response code="500">Erro ao atualizar os dados no Banco de Dados</response>
    [HttpPut("{companyId:int}")]
    public async Task<ActionResult<Company>> UpdateCompany(int companyId, [FromBody] Company company)
    {
        try
        {
            if (company == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            company.id_company = companyId;

            var updatedCompany = await _companyRepository.UpdateCompany(company);

            if (updatedCompany == null)
            {
                return NotFound($"Empresa com o Id: {companyId}, não foi encontrada");
            }

            return Ok(updatedCompany);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta uma empresa pelo ID.
    /// </summary>
    /// <param name="companyId">O ID da empresa a ser deletada.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se a empresa não for encontrada</response>
    /// <response code="500">Erro ao deletar os dados no Banco de Dados</response>
    [HttpDelete("{companyId:int}")]
    public async Task<ActionResult<Company>> DeleteCompany(int companyId)
    {
        try
        {
            var deletedCompany = await _companyRepository.GetCompanyById(companyId);

            if (deletedCompany == null)
            {
                return NotFound($"Empresa com o Id: {companyId}, não foi encontrada");
            }

            _companyRepository.DeleteCompany(companyId);
            return Ok("Empresa, foi deletado(a) com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados no Banco de Dados");
        }
    }
}