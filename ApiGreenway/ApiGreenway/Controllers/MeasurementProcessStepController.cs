using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/measurement-process-step")]
[ApiController]
public class MeasurementProcessStepController : ControllerBase
{
    private readonly IMeasurementProcessStepRepository _measurementProcessStepRepository;

    public MeasurementProcessStepController(IMeasurementProcessStepRepository measurementProcessStepRepository)
    {
        this._measurementProcessStepRepository = measurementProcessStepRepository;
    }

    /// <summary>
    /// Obtém todos os passos do processo de medição.
    /// </summary>
    /// <returns>Uma lista de passos do processo de medição.</returns>
    /// <response code="200">Retorna a lista de passos do processo de medição</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeasurementProcessStep>>> GetMeasurementProcessSteps()
    {
        try
        {
            var measurementProcessSteps = await _measurementProcessStepRepository.GetMeasurementProcessSteps();
            return Ok(measurementProcessSteps);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém um passo do processo de medição pelo ID.
    /// </summary>
    /// <param name="measurementProcessStepId">O ID do passo do processo de medição.</param>
    /// <returns>Um passo do processo de medição específico.</returns>
    /// <response code="200">Retorna o passo do processo de medição com o ID fornecido</response>
    /// <response code="404">Se o passo do processo de medição não for encontrado</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{measurementProcessStepId:int}")]
    public async Task<ActionResult<MeasurementProcessStep>> GetMeasurementProcessStepById(int measurementProcessStepId)
    {
        try
        {
            var measurementProcessStep = await _measurementProcessStepRepository.GetMeasurementProcessStepById(measurementProcessStepId);
            if (measurementProcessStep == null)
            {
                return NotFound($"Medição de Processo com o ID: {measurementProcessStepId}, não foi encontrado(a)!");
            }

            return Ok(measurementProcessStep);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria um novo passo do processo de medição.
    /// </summary>
    /// <param name="measurementProcessStep">Os dados do passo do processo de medição a ser criado.</param>
    /// <returns>O passo do processo de medição criado.</returns>
    /// <response code="201">Retorna o passo do processo de medição criado</response>
    /// <response code="400">Se os dados do passo do processo de medição forem inválidos</response>
    /// <response code="500">Erro ao salvar os dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<MeasurementProcessStep>> CreateMeasurementProcessStep([FromBody] MeasurementProcessStep measurementProcessStep)
    {
        try
        {
            if (measurementProcessStep == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdMeasurementProcessStep = await _measurementProcessStepRepository.AddMeasurementProcessStep(measurementProcessStep);
            return CreatedAtAction(nameof(GetMeasurementProcessStepById), new
            {
                measurementProcessStepId = createdMeasurementProcessStep.id_measurement_process_step
            }, createdMeasurementProcessStep);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza um passo do processo de medição existente.
    /// </summary>
    /// <param name="measurementProcessStepId">O ID do passo do processo de medição a ser atualizado.</param>
    /// <param name="measurementProcessStep">Os novos dados do passo do processo de medição.</param>
    /// <returns>O passo do processo de medição atualizado.</returns>
    /// <response code="200">Retorna o passo do processo de medição atualizado</response>
    /// <response code="404">Se o passo do processo de medição não for encontrado</response>
    /// <response code="400">Se os dados do passo do processo de medição forem inválidos</response>
    /// <response code="500">Erro ao atualizar os dados no Banco de Dados</response>
    [HttpPut("{measurementProcessStepId:int}")]
    public async Task<ActionResult<MeasurementProcessStep>> UpdateMeasurementProcessStep(int measurementProcessStepId, [FromBody] MeasurementProcessStep measurementProcessStep)
    {
        try
        {
            if (measurementProcessStep == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            measurementProcessStep.id_measurement_process_step = measurementProcessStepId;
            var updatedMeasurementProcessStep = await _measurementProcessStepRepository.UpdateMeasurementProcessStep(measurementProcessStep);

            if (updatedMeasurementProcessStep == null)
            {
                return NotFound($"Medição de Processo com o ID: {measurementProcessStepId}, não foi encontrado(a)!");
            }

            return Ok(updatedMeasurementProcessStep);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta um passo do processo de medição pelo ID.
    /// </summary>
    /// <param name="measurementProcessStepId">O ID do passo do processo de medição a ser deletado.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se o passo do processo de medição não for encontrado</response>
    /// <response code="500">Erro ao deletar os dados no Banco de Dados</response>
    [HttpDelete("{measurementProcessStepId:int}")]
    public async Task<ActionResult<MeasurementProcessStep>> DeleteMeasurementProcessStep(int measurementProcessStepId)
    {
        try
        {
            var deletedMeasurementProcessStep = await _measurementProcessStepRepository.GetMeasurementProcessStepById(measurementProcessStepId);

            if (deletedMeasurementProcessStep == null)
            {
                return NotFound($"Medição de Processo com o ID: {measurementProcessStepId}, não foi encontrado(a)!");
            }

            _measurementProcessStepRepository.DeleteMeasurementProcessStep(measurementProcessStepId);

            return Ok("Medição de Processos, foi deletado(a) com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados no Banco de Dados");
        }
    }
}