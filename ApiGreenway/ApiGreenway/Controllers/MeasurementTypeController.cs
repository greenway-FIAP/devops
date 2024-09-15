using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/measurement-type")]
    [ApiController]
    public class MeasurementTypeController : ControllerBase
    {
        private readonly IMeasurementTypeRepository _measurementTypeRepository;

        public MeasurementTypeController(IMeasurementTypeRepository measurementTypeRepository)
        {
            this._measurementTypeRepository = measurementTypeRepository;
        }

        /// <summary>
        /// Obtém todos os tipos de medição.
        /// </summary>
        /// <returns>Uma lista de tipos de medição.</returns>
        /// <response code="200">Retorna a lista de tipos de medição.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementType>>> GetMeasurementTypes()
        {
            try
            {
                var measurementTypes = await _measurementTypeRepository.GetMeasurementTypes();
                return Ok(measurementTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um tipo de medição pelo ID.
        /// </summary>
        /// <param name="measurementTypeId">O ID do tipo de medição.</param>
        /// <returns>O tipo de medição correspondente ao ID.</returns>
        /// <response code="200">Retorna o tipo de medição encontrado.</response>
        /// <response code="404">Tipo de medição não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{measurementTypeId:int}")]
        public async Task<ActionResult<MeasurementType>> GetMeasurementTypeById(int measurementTypeId)
        {
            try
            {
                var measurementType = await _measurementTypeRepository.GetMeasurementTypeById(measurementTypeId);

                if (measurementType == null)
                {
                    return NotFound($"Tipo de Medição com o ID: {measurementTypeId}, não foi encontrado(a)!");
                }

                return Ok(measurementType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo tipo de medição.
        /// </summary>
        /// <param name="measurementType">O tipo de medição a ser criado.</param>
        /// <returns>O tipo de medição criado.</returns>
        /// <response code="201">Tipo de medição criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<MeasurementType>> CreateMeasurementType([FromBody] MeasurementType measurementType)
        {
            try
            {
                if (measurementType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdMeasurementType = await _measurementTypeRepository.AddMeasurementType(measurementType);
                return CreatedAtAction(nameof(GetMeasurementTypeById), new
                {
                    measurementTypeId = createdMeasurementType.id_measurement_type
                }, createdMeasurementType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um tipo de medição existente.
        /// </summary>
        /// <param name="measurementTypeId">O ID do tipo de medição a ser atualizado.</param>
        /// <param name="measurementType">Os novos dados do tipo de medição.</param>
        /// <returns>O tipo de medição atualizado.</returns>
        /// <response code="200">Tipo de medição atualizado com sucesso.</response>
        /// <response code="404">Tipo de medição não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{measurementTypeId:int}")]
        public async Task<ActionResult<MeasurementType>> UpdateMeasurementType(int measurementTypeId, [FromBody] MeasurementType measurementType)
        {
            try
            {
                if (measurementType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                measurementType.id_measurement_type = measurementTypeId;
                var updatedMeasurementType = await _measurementTypeRepository.UpdateMeasurementType(measurementType);

                if (updatedMeasurementType == null)
                {
                    return NotFound($"Tipo de Medição com o ID: {measurementTypeId}, não foi encontrado(a)!");
                }

                return Ok(updatedMeasurementType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um tipo de medição pelo ID.
        /// </summary>
        /// <param name="measurementTypeId">O ID do tipo de medição a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Tipo de medição deletado com sucesso.</response>
        /// <response code="404">Tipo de medição não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{measurementTypeId:int}")]
        public async Task<ActionResult<MeasurementType>> DeleteMeasurementType(int measurementTypeId)
        {
            try
            {
                var deletedMeasurementType = await _measurementTypeRepository.GetMeasurementTypeById(measurementTypeId);

                if (deletedMeasurementType == null)
                {
                    return NotFound($"Tipo de Medição com o ID: {measurementTypeId}, não foi encontrado(a)!");
                }

                _measurementTypeRepository.DeleteMeasurementType(measurementTypeId);
                return Ok("Tipo de Medição, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}