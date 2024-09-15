using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/sustainable-improvement-actions")]
    [ApiController]
    public class SustainableImprovementActionsController : ControllerBase
    {
        private readonly ISustainableImprovementActionsRepository _sustainableImprovementActionsRepository;

        public SustainableImprovementActionsController(ISustainableImprovementActionsRepository sustainableImprovementActionsRepository)
        {
            this._sustainableImprovementActionsRepository = sustainableImprovementActionsRepository;
        }

        /// <summary>
        /// Obtém todas as ações de melhoria sustentável.
        /// </summary>
        /// <returns>Uma lista de ações de melhoria sustentável.</returns>
        /// <response code="200">Retorna a lista de ações de melhoria sustentável.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SustainableImprovementActions>>> GetSustainableImprovementActions()
        {
            try
            {
                var sustainableImprovementActions = await _sustainableImprovementActionsRepository.GetSustainableImprovementActions();
                return Ok(sustainableImprovementActions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém uma ação de melhoria sustentável pelo ID.
        /// </summary>
        /// <param name="sustainableImprovementActionsId">O ID da ação de melhoria sustentável.</param>
        /// <returns>A ação de melhoria sustentável correspondente ao ID.</returns>
        /// <response code="200">Retorna a ação de melhoria sustentável encontrada.</response>
        /// <response code="404">Ação de melhoria sustentável não encontrada.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{sustainableImprovementActionsId:int}")]
        public async Task<ActionResult<SustainableImprovementActions>> GetSustainableImprovementActionsById(int sustainableImprovementActionsId)
        {
            try
            {
                var sustainableImprovementActions = await _sustainableImprovementActionsRepository.GetSustainableImprovementActionsById(sustainableImprovementActionsId);

                if (sustainableImprovementActions == null)
                {
                    return NotFound($"Ação de Melhoria Sustentável com o ID: {sustainableImprovementActionsId}, não foi encontrada!");
                }

                return Ok(sustainableImprovementActions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria uma nova ação de melhoria sustentável.
        /// </summary>
        /// <param name="sustainableImprovementActions">A ação de melhoria sustentável a ser criada.</param>
        /// <returns>A ação de melhoria sustentável criada.</returns>
        /// <response code="201">Ação de melhoria sustentável criada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<SustainableImprovementActions>> CreateSustainableImprovementActions([FromBody] SustainableImprovementActions sustainableImprovementActions)
        {
            try
            {
                if (sustainableImprovementActions == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdSustainableImprovementActions = await _sustainableImprovementActionsRepository.AddSustainableImprovementActions(sustainableImprovementActions);
                return CreatedAtAction(nameof(GetSustainableImprovementActionsById), new
                {
                    sustainableImprovementActionsId = createdSustainableImprovementActions.id_sustainable_improvement_action
                }, createdSustainableImprovementActions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza uma ação de melhoria sustentável existente.
        /// </summary>
        /// <param name="sustainableImprovementActionsId">O ID da ação a ser atualizada.</param>
        /// <param name="sustainableImprovementActions">Os novos dados da ação de melhoria sustentável.</param>
        /// <returns>A ação de melhoria sustentável atualizada.</returns>
        /// <response code="200">Ação de melhoria sustentável atualizada com sucesso.</response>
        /// <response code="404">Ação de melhoria sustentável não encontrada.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{sustainableImprovementActionsId:int}")]
        public async Task<ActionResult<SustainableImprovementActions>> UpdateSustainableImprovementActions(int sustainableImprovementActionsId, [FromBody] SustainableImprovementActions sustainableImprovementActions)
        {
            try
            {
                if (sustainableImprovementActions == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                sustainableImprovementActions.id_sustainable_improvement_action = sustainableImprovementActionsId;
                var updatedSustainableImprovementActions = await _sustainableImprovementActionsRepository.UpdateSustainableImprovementActions(sustainableImprovementActions);

                if (updatedSustainableImprovementActions == null)
                {
                    return NotFound($"Ação de Melhoria Sustentável com o ID: {sustainableImprovementActionsId}, não foi encontrada!");
                }

                return Ok(updatedSustainableImprovementActions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta uma ação de melhoria sustentável pelo ID.
        /// </summary>
        /// <param name="sustainableImprovementActionsId">O ID da ação a ser deletada.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Ação de melhoria sustentável deletada com sucesso.</response>
        /// <response code="404">Ação de melhoria sustentável não encontrada.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{sustainableImprovementActionsId:int}")]
        public async Task<ActionResult<SustainableImprovementActions>> DeleteSustainableImprovementActions(int sustainableImprovementActionsId)
        {
            try
            {
                var deletedSustainableImprovementActions = await _sustainableImprovementActionsRepository.GetSustainableImprovementActionsById(sustainableImprovementActionsId);
                if (deletedSustainableImprovementActions == null)
                {
                    return NotFound($"Ação de Melhoria Sustentável com o ID: {sustainableImprovementActionsId}, não foi encontrada!");
                }

                _sustainableImprovementActionsRepository.DeleteSustainableImprovementActions(sustainableImprovementActionsId);
                return Ok("Ação de Melhoria Sustentável, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}