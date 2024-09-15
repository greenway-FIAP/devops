using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/improvement-measurement")]
[ApiController]
public class ImprovementMeasurementController : ControllerBase
{
    private readonly IImprovementMeasurementRepository _improvementMeasurementRepository;

    public ImprovementMeasurementController(IImprovementMeasurementRepository improvementMeasurementRepository)
    {
        this._improvementMeasurementRepository = improvementMeasurementRepository;
    }

    /// <summary>
    /// Obtém todas as medições de melhoria.
    /// </summary>
    /// <returns>Uma lista de medições de melhoria.</returns>
    /// <response code="200">Retorna a lista de medições de melhoria</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ImprovementMeasurement>>> GetImprovementMeasurements()
    {
        try
        {
            var improvementMeasurements = await _improvementMeasurementRepository.GetImprovementMeasurements();
            return Ok(improvementMeasurements);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém uma medição de melhoria pelo ID.
    /// </summary>
    /// <param name="improvementMeasurementId">O ID da medição de melhoria.</param>
    /// <returns>Uma medição de melhoria específica.</returns>
    /// <response code="200">Retorna a medição de melhoria com o ID fornecido</response>
    /// <response code="404">Se a medição de melhoria não for encontrada</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{improvementMeasurementId:int}")]
    public async Task<ActionResult<ImprovementMeasurement>> GetImprovementMeasurementById(int improvementMeasurementId)
    {
        try
        {
            var improvementMeasurement = await _improvementMeasurementRepository.GetImprovementMeasurementById(improvementMeasurementId);
            if (improvementMeasurement == null)
            {
                return NotFound($"Medição de Melhoria com o ID: {improvementMeasurementId}, não foi encontrada!");
            }

            return Ok(improvementMeasurement);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria uma nova medição de melhoria.
    /// </summary>
    /// <param name="improvementMeasurement">Os dados da medição de melhoria a ser criada.</param>
    /// <returns>A medição de melhoria criada.</returns>
    /// <response code="201">Retorna a medição de melhoria criada</response>
    /// <response code="400">Se os dados da medição de melhoria forem inválidos</response>
    /// <response code="500">Erro ao adicionar dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<ImprovementMeasurement>> CreateImprovementMeasurement([FromBody] ImprovementMeasurement improvementMeasurement)
    {
        try
        {
            if (improvementMeasurement == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdImprovementMeasurement = await _improvementMeasurementRepository.AddImprovementMeasurement(improvementMeasurement);
            return CreatedAtAction(nameof(GetImprovementMeasurementById), new
            {
                improvementMeasurementId = createdImprovementMeasurement.id_improvement_measurement
            }, createdImprovementMeasurement);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza uma medição de melhoria existente.
    /// </summary>
    /// <param name="improvementMeasurementId">O ID da medição de melhoria a ser atualizada.</param>
    /// <param name="improvementMeasurement">Os novos dados da medição de melhoria.</param>
    /// <returns>A medição de melhoria atualizada.</returns>
    /// <response code="200">Retorna a medição de melhoria atualizada</response>
    /// <response code="404">Se a medição de melhoria não for encontrada</response>
    /// <response code="400">Se os dados da medição de melhoria forem inválidos</response>
    /// <response code="500">Erro ao atualizar os dados no Banco de Dados</response>
    [HttpPut("{improvementMeasurementId:int}")]
    public async Task<ActionResult<ImprovementMeasurement>> UpdateImprovementMeasurement(int improvementMeasurementId, [FromBody] ImprovementMeasurement improvementMeasurement)
    {
        try
        {
            if (improvementMeasurement == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            improvementMeasurement.id_improvement_measurement = improvementMeasurementId;
            var updatedImprovementMeasurement = await _improvementMeasurementRepository.UpdateImprovementMeasurement(improvementMeasurement);

            if (updatedImprovementMeasurement == null)
            {
                return NotFound($"Medição de Melhoria com o ID: {improvementMeasurementId}, não foi encontrada!");
            }

            return Ok(updatedImprovementMeasurement);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta uma medição de melhoria pelo ID.
    /// </summary>
    /// <param name="improvementMeasurementId">O ID da medição de melhoria a ser deletada.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se a medição de melhoria não for encontrada</response>
    /// <response code="500">Erro ao deletar os dados do Banco de Dados</response>
    [HttpDelete("{improvementMeasurementId:int}")]
    public async Task<ActionResult<ImprovementMeasurement>> DeleteImprovementMeasurement(int improvementMeasurementId)
    {
        try
        {
            var deletedMeasurement = await _improvementMeasurementRepository.GetImprovementMeasurementById(improvementMeasurementId);
            if (deletedMeasurement == null)
            {
                return NotFound($"Medição de Melhoria com o ID: {improvementMeasurementId}, não foi encontrada!");
            }
            _improvementMeasurementRepository.DeleteImprovementMeasurement(improvementMeasurementId);

            return Ok("Medição de Melhoria, foi deletada com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados do Banco de Dados");
        }
    }
}