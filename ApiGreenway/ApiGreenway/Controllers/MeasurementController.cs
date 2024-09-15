using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/measurement")]
[ApiController]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurementRepository _measurementRepository;

    public MeasurementController(IMeasurementRepository measurementRepository)
    {
        this._measurementRepository = measurementRepository;
    }

    /// <summary>
    /// Obtém todas as medições.
    /// </summary>
    /// <returns>Uma lista de medições.</returns>
    /// <response code="200">Retorna a lista de medições</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurements()
    {
        try
        {
            var measurements = await _measurementRepository.GetMeasurements();
            return Ok(measurements);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém uma medição pelo ID.
    /// </summary>
    /// <param name="measurementId">O ID da medição.</param>
    /// <returns>Uma medição específica.</returns>
    /// <response code="200">Retorna a medição com o ID fornecido</response>
    /// <response code="404">Se a medição não for encontrada</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{measurementId:int}")]
    public async Task<ActionResult<Measurement>> GetMeasurementById(int measurementId)
    {
        try
        {
            var measurement = await _measurementRepository.GetMeasurementById(measurementId);
            if (measurement == null)
            {
                return NotFound($"Medição com o ID: {measurementId}, não foi encontrada!");
            }

            return Ok(measurement);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria uma nova medição.
    /// </summary>
    /// <param name="measurement">Os dados da medição a ser criada.</param>
    /// <returns>A medição criada.</returns>
    /// <response code="201">Retorna a medição criada</response>
    /// <response code="400">Se os dados da medição forem inválidos</response>
    /// <response code="500">Erro ao adicionar dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<Measurement>> CreateMeasurement([FromBody] Measurement measurement)
    {
        try
        {
            if (measurement == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdMeasurement = await _measurementRepository.AddMeasurement(measurement);
            return CreatedAtAction(nameof(GetMeasurementById), new
            {
                measurementId = createdMeasurement.id_measurement
            }, createdMeasurement);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza uma medição existente.
    /// </summary>
    /// <param name="measurementId">O ID da medição a ser atualizada.</param>
    /// <param name="measurement">Os novos dados da medição.</param>
    /// <returns>A medição atualizada.</returns>
    /// <response code="200">Retorna a medição atualizada</response>
    /// <response code="404">Se a medição não for encontrada</response>
    /// <response code="400">Se os dados da medição forem inválidos</response>
    /// <response code="500">Erro ao atualizar dados no Banco de Dados</response>
    [HttpPut("{measurementId:int}")]
    public async Task<ActionResult<Measurement>> UpdateMeasurement(int measurementId, [FromBody] Measurement measurement)
    {
        try
        {
            if (measurement == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            measurement.id_measurement = measurementId;
            var updatedMeasurement = await _measurementRepository.UpdateMeasurement(measurement);

            if (updatedMeasurement == null)
            {
                return NotFound($"Medição com o ID: {measurementId}, não foi encontrada!");
            }

            return Ok(updatedMeasurement);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta uma medição pelo ID.
    /// </summary>
    /// <param name="measurementId">O ID da medição a ser deletada.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se a medição não for encontrada</response>
    /// <response code="500">Erro ao deletar dados no Banco de Dados</response>
    [HttpDelete("{measurementId:int}")]
    public async Task<ActionResult<Measurement>> DeleteMeasurement(int measurementId)
    {
        try
        {
            var deletedMeasurement = await _measurementRepository.GetMeasurementById(measurementId);

            if (deletedMeasurement == null)
            {
                return NotFound($"Medição com o ID: {measurementId}, não foi encontrada!");
            }

            _measurementRepository.DeleteMeasurement(measurementId);
            return Ok("Medição, foi deletada com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
        }
    }
}