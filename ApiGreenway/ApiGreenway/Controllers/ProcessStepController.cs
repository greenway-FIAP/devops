using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/process-step")]
    [ApiController]
    public class ProcessStepController : ControllerBase
    {
        private readonly IProcessStepRepository _processStepRepository;

        public ProcessStepController(IProcessStepRepository processStepRepository)
        {
            this._processStepRepository = processStepRepository;
        }

        /// <summary>
        /// Obtém todas as etapas do processo.
        /// </summary>
        /// <returns>Uma lista de etapas do processo.</returns>
        /// <response code="200">Retorna a lista de etapas do processo.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessStep>>> GetProcessSteps()
        {
            try
            {
                var processSteps = await _processStepRepository.GetProcessSteps();
                return Ok(processSteps);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém uma etapa do processo pelo ID.
        /// </summary>
        /// <param name="processStepId">O ID da etapa do processo.</param>
        /// <returns>A etapa do processo correspondente ao ID.</returns>
        /// <response code="200">Retorna a etapa do processo encontrada.</response>
        /// <response code="404">Etapa do processo não encontrada.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{processStepId:int}")]
        public async Task<ActionResult<ProcessStep>> GetProcessStepById(int processStepId)
        {
            try
            {
                var processStep = await _processStepRepository.GetProcessStepById(processStepId);
                if (processStep == null)
                {
                    return NotFound($"Etapa do Processo com o ID: {processStepId}, não foi encontrado(a)!");
                }

                return Ok(processStep);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria uma nova etapa do processo.
        /// </summary>
        /// <param name="processStep">A etapa do processo a ser criada.</param>
        /// <returns>A etapa do processo criada.</returns>
        /// <response code="201">Etapa do processo criada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<ProcessStep>> CreateProcessStep([FromBody] ProcessStep processStep)
        {
            try
            {
                if (processStep == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdProcessStep = await _processStepRepository.AddProcessStep(processStep);
                return CreatedAtAction(nameof(GetProcessStepById), new
                {
                    processStepId = createdProcessStep.id_process_step
                }, createdProcessStep);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza uma etapa do processo existente.
        /// </summary>
        /// <param name="processStepId">O ID da etapa do processo a ser atualizada.</param>
        /// <param name="processStep">Os novos dados da etapa do processo.</param>
        /// <returns>A etapa do processo atualizada.</returns>
        /// <response code="200">Etapa do processo atualizada com sucesso.</response>
        /// <response code="404">Etapa do processo não encontrada.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{processStepId:int}")]
        public async Task<ActionResult<ProcessStep>> UpdateProcessStep(int processStepId, [FromBody] ProcessStep processStep)
        {
            try
            {
                if (processStep == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                processStep.id_process_step = processStepId;
                var updatedProcessStep = await _processStepRepository.UpdateProcessStep(processStep);

                if (updatedProcessStep == null)
                {
                    return NotFound($"Etapa do Processo com o ID: {processStepId}, não foi encontrado(a)!");
                }

                return Ok(updatedProcessStep);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta uma etapa do processo pelo ID.
        /// </summary>
        /// <param name="processStepId">O ID da etapa do processo a ser deletada.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Etapa do processo deletada com sucesso.</response>
        /// <response code="404">Etapa do processo não encontrada.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{processStepId:int}")]
        public async Task<ActionResult<ProcessStep>> DeleteProcessStep(int processStepId)
        {
            try
            {
                var deletedProcessStep = await _processStepRepository.GetProcessStepById(processStepId);
                if (deletedProcessStep == null)
                {
                    return NotFound($"Etapa do Processo com o ID: {processStepId}, não foi encontrado(a)!");
                }

                _processStepRepository.DeleteProcessStep(processStepId);
                return Ok("Etapa do Processo, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}