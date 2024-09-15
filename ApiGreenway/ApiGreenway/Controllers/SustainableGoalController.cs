using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/sustainable-goal")]
    [ApiController]
    public class SustainableGoalController : ControllerBase
    {
        private readonly ISustainableGoalRepository _sustainableGoalRepository;

        public SustainableGoalController(ISustainableGoalRepository sustainableGoalRepository)
        {
            this._sustainableGoalRepository = sustainableGoalRepository;
        }

        /// <summary>
        /// Obtém todas as metas sustentáveis.
        /// </summary>
        /// <returns>Uma lista de metas sustentáveis.</returns>
        /// <response code="200">Retorna a lista de metas sustentáveis.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SustainableGoal>>> GetSustainableGoals()
        {
            try
            {
                var sustainableGoals = await _sustainableGoalRepository.GetSustainableGoals();
                return Ok(sustainableGoals);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém uma meta sustentável pelo ID.
        /// </summary>
        /// <param name="sustainableGoalId">O ID da meta sustentável.</param>
        /// <returns>A meta sustentável correspondente ao ID.</returns>
        /// <response code="200">Retorna a meta sustentável encontrada.</response>
        /// <response code="404">Meta sustentável não encontrada.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{sustainableGoalId:int}")]
        public async Task<ActionResult<SustainableGoal>> GetSustainableGoalById(int sustainableGoalId)
        {
            try
            {
                var sustainableGoal = await _sustainableGoalRepository.GetSustainableGoalById(sustainableGoalId);

                if (sustainableGoal == null)
                {
                    return NotFound($"Meta Sustentável com o ID: {sustainableGoalId}, não encontrado(a)!");
                }

                return Ok(sustainableGoal);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria uma nova meta sustentável.
        /// </summary>
        /// <param name="sustainableGoal">A meta sustentável a ser criada.</param>
        /// <returns>A meta sustentável criada.</returns>
        /// <response code="201">Meta sustentável criada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<SustainableGoal>> CreateSustainableGoal([FromBody] SustainableGoal sustainableGoal)
        {
            try
            {
                if (sustainableGoal == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdSustainableGoal = await _sustainableGoalRepository.AddSustainableGoal(sustainableGoal);
                return CreatedAtAction(nameof(GetSustainableGoalById), new
                {
                    sustainableGoalId = createdSustainableGoal.id_sustainable_goal
                }, createdSustainableGoal);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza uma meta sustentável existente.
        /// </summary>
        /// <param name="sustainableGoalId">O ID da meta sustentável a ser atualizada.</param>
        /// <param name="sustainableGoal">Os novos dados da meta sustentável.</param>
        /// <returns>A meta sustentável atualizada.</returns>
        /// <response code="200">Meta sustentável atualizada com sucesso.</response>
        /// <response code="404">Meta sustentável não encontrada.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{sustainableGoalId:int}")]
        public async Task<ActionResult<SustainableGoal>> UpdateSustainableGoal(int sustainableGoalId, [FromBody] SustainableGoal sustainableGoal)
        {
            try
            {
                if (sustainableGoal == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                sustainableGoal.id_sustainable_goal = sustainableGoalId;
                var updatedSustainableGoal = await _sustainableGoalRepository.UpdateSustainableGoal(sustainableGoal);

                if (updatedSustainableGoal == null)
                {
                    return NotFound($"Meta Sustentável com o ID: {sustainableGoalId}, não encontrado(a)!");
                }

                return Ok(updatedSustainableGoal);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta uma meta sustentável pelo ID.
        /// </summary>
        /// <param name="sustainableGoalId">O ID da meta sustentável a ser deletada.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Meta sustentável deletada com sucesso.</response>
        /// <response code="404">Meta sustentável não encontrada.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{sustainableGoalId:int}")]
        public async Task<ActionResult<SustainableGoal>> DeleteSustainableGoal(int sustainableGoalId)
        {
            try
            {
                var deletedSustainableGoal = await _sustainableGoalRepository.GetSustainableGoalById(sustainableGoalId);

                if (deletedSustainableGoal == null)
                {
                    return NotFound($"Meta Sustentável com o ID: {sustainableGoalId}, não encontrado(a)!");
                }

                _sustainableGoalRepository.DeleteSustainableGoal(sustainableGoalId);
                return Ok("Meta Sustentável, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}