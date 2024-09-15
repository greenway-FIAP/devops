using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/step")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IStepRepository _stepRepository;

        public StepController(IStepRepository stepRepository)
        {
            this._stepRepository = stepRepository;
        }

        /// <summary>
        /// Obtém todas as etapas.
        /// </summary>
        /// <returns>Uma lista de etapas.</returns>
        /// <response code="200">Retorna a lista de etapas.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Step>>> GetSteps()
        {
            try
            {
                var steps = await _stepRepository.GetSteps();
                return Ok(steps);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém uma etapa pelo ID.
        /// </summary>
        /// <param name="stepId">O ID da etapa.</param>
        /// <returns>A etapa correspondente ao ID.</returns>
        /// <response code="200">Retorna a etapa encontrada.</response>
        /// <response code="404">Etapa não encontrada.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{stepId:int}")]
        public async Task<ActionResult<Step>> GetStepById(int stepId)
        {
            try
            {
                var step = await _stepRepository.GetStepById(stepId);
                if (step == null)
                {
                    return NotFound($"Etapa com o ID: {stepId}, não foi encontrado(a)!");
                }

                return Ok(step);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria uma nova etapa.
        /// </summary>
        /// <param name="step">A etapa a ser criada.</param>
        /// <returns>A etapa criada.</returns>
        /// <response code="201">Etapa criada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<Step>> CreateStep([FromBody] Step step)
        {
            try
            {
                if (step == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdStep = await _stepRepository.AddStep(step);
                return CreatedAtAction(nameof(GetStepById), new
                {
                    stepId = createdStep.id_step
                }, createdStep);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza uma etapa existente.
        /// </summary>
        /// <param name="stepId">O ID da etapa a ser atualizada.</param>
        /// <param name="step">Os novos dados da etapa.</param>
        /// <returns>A etapa atualizada.</returns>
        /// <response code="200">Etapa atualizada com sucesso.</response>
        /// <response code="404">Etapa não encontrada.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{stepId:int}")]
        public async Task<ActionResult<Step>> UpdateStep(int stepId, [FromBody] Step step)
        {
            try
            {
                if (step == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                step.id_step = stepId;
                var updatedStep = await _stepRepository.UpdateStep(step);

                if (updatedStep == null)
                {
                    return NotFound($"Etapa com o ID: {stepId}, não foi encontrado(a)!");
                }

                return Ok(updatedStep);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta uma etapa pelo ID.
        /// </summary>
        /// <param name="stepId">O ID da etapa a ser deletada.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Etapa deletada com sucesso.</response>
        /// <response code="404">Etapa não encontrada.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{stepId:int}")]
        public async Task<ActionResult<Step>> DeleteStep(int stepId)
        {
            try
            {
                var deletedStep = await _stepRepository.GetStepById(stepId);

                if (deletedStep == null)
                {
                    return NotFound($"Etapa com o ID: {stepId}, não foi encontrado(a)!");
                }

                _stepRepository.DeleteStep(stepId);
                return Ok("Etapa, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}