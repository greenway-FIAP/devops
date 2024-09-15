using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/company-representative")]
[ApiController]
public class CompanyRepresentativeController : ControllerBase
{
    private readonly ICompanyRepresentativeRepository _companyRepresentativeRepository;

    public CompanyRepresentativeController(ICompanyRepresentativeRepository companyRepresentativeRepository)
    {
        this._companyRepresentativeRepository = companyRepresentativeRepository;
    }

    /// <summary>
    /// Obtém todos os representantes das empresas.
    /// </summary>
    /// <returns>Uma lista de representantes das empresas.</returns>
    /// <response code="200">Retorna a lista de representantes das empresas</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyRepresentative>>> GetCompanyRepresentatives()
    {
        try
        {
            var companyRepresentatives = await _companyRepresentativeRepository.GetCompanyRepresentatives();
            return Ok(companyRepresentatives);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém um representante da empresa pelo ID.
    /// </summary>
    /// <param name="companyRepresentativeId">O ID do representante da empresa.</param>
    /// <returns>Um representante da empresa específico.</returns>
    /// <response code="200">Retorna o representante da empresa com o ID fornecido</response>
    /// <response code="404">Se o representante da empresa não for encontrado</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{companyRepresentativeId:int}")]
    public async Task<ActionResult<CompanyRepresentative>> GetCompanyRepresentativeById(int companyRepresentativeId)
    {
        try
        {
            var companyRepresentative = await _companyRepresentativeRepository.GetCompanyRepresentativeById(companyRepresentativeId);
            if (companyRepresentative == null)
            {
                return NotFound($"Representante da Empresa com o ID: {companyRepresentativeId}, não foi encontrado(a)!");
            }

            return Ok(companyRepresentative);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria um novo representante da empresa.
    /// </summary>
    /// <param name="companyRepresentative">Os dados do representante da empresa a ser criado.</param>
    /// <returns>O representante da empresa criado.</returns>
    /// <response code="201">Retorna o representante da empresa criado</response>
    /// <response code="400">Se os dados do representante da empresa forem inválidos</response>
    /// <response code="500">Erro ao salvar os dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<CompanyRepresentative>> CreateCompanyRepresentative([FromBody] CompanyRepresentative companyRepresentative)
    {
        try
        {
            if (companyRepresentative == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdCompanyRepresentative = await _companyRepresentativeRepository.AddCompanyRepresentative(companyRepresentative);
            return CreatedAtAction(nameof(GetCompanyRepresentativeById), new
            {
                companyRepresentativeId = createdCompanyRepresentative.id_company_representative
            }, createdCompanyRepresentative);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza um representante da empresa existente.
    /// </summary>
    /// <param name="companyRepresentativeId">O ID do representante da empresa a ser atualizado.</param>
    /// <param name="companyRepresentative">Os novos dados do representante da empresa.</param>
    /// <returns>O representante da empresa atualizado.</returns>
    /// <response code="200">Retorna o representante da empresa atualizado</response>
    /// <response code="404">Se o representante da empresa não for encontrado</response>
    /// <response code="400">Se os dados do representante da empresa forem inválidos</response>
    /// <response code="500">Erro ao atualizar os dados no Banco de Dados</response>
    [HttpPut("{companyRepresentativeId:int}")]
    public async Task<ActionResult<CompanyRepresentative>> UpdateCompanyRepresentative(int companyRepresentativeId, [FromBody] CompanyRepresentative companyRepresentative)
    {
        try
        {
            if (companyRepresentative == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            companyRepresentative.id_company_representative = companyRepresentativeId;
            var updateCompanyRepresentative = await _companyRepresentativeRepository.UpdateCompanyRepresentative(companyRepresentative);

            if (updateCompanyRepresentative == null)
            {
                return NotFound($"Representante da Empresa com o ID: {companyRepresentativeId}, não foi encontrado(a)!");
            }

            return Ok(updateCompanyRepresentative);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta um representante da empresa pelo ID.
    /// </summary>
    /// <param name="companyRepresentativeId">O ID do representante da empresa a ser deletado.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se o representante da empresa não for encontrado</response>
    /// <response code="500">Erro ao deletar os dados no Banco de Dados</response>
    [HttpDelete("{companyRepresentativeId:int}")]
    public async Task<ActionResult<CompanyRepresentative>> DeleteCompanyRepresentative(int companyRepresentativeId)
    {
        try
        {
            var companyRepresentativeToDelete = await _companyRepresentativeRepository.GetCompanyRepresentativeById(companyRepresentativeId);
            if (companyRepresentativeToDelete == null)
            {
                return NotFound($"Representante da Empresa com o ID: {companyRepresentativeId}, não foi encontrado(a)!");
            }
            _companyRepresentativeRepository.DeleteCompanyRepresentative(companyRepresentativeId);
            return Ok("Representante da Empresa, foi deletado(a) com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados no Banco de Dados");
        }
    }
}