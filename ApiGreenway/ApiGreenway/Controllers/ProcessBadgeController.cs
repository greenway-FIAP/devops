using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/process-badge")]
    [ApiController]
    public class ProcessBadgeController : ControllerBase
    {
        private readonly IProcessBadgeRepository _processBadgeRepository;

        public ProcessBadgeController(IProcessBadgeRepository processBadgeRepository)
        {
            this._processBadgeRepository = processBadgeRepository;
        }

        /// <summary>
        /// Obtém todos os processos de badge.
        /// </summary>
        /// <returns>Uma lista de processos de badge.</returns>
        /// <response code="200">Retorna a lista de processos de badge.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessBadge>>> GetProcessBadges()
        {
            try
            {
                var processBadges = await _processBadgeRepository.GetProcessBadges();
                return Ok(processBadges);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um processo de badge pelo ID.
        /// </summary>
        /// <param name="processBadgeId">O ID do processo de badge.</param>
        /// <returns>O processo de badge correspondente ao ID.</returns>
        /// <response code="200">Retorna o processo de badge encontrado.</response>
        /// <response code="404">Processo de badge não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{processBadgeId:int}")]
        public async Task<ActionResult<ProcessBadge>> GetProcessBadgeById(int processBadgeId)
        {
            try
            {
                var processBadge = await _processBadgeRepository.GetProcessBadgeById(processBadgeId);
                if (processBadge == null)
                {
                    return NotFound($"Processo da Badge com o ID: {processBadgeId}, não foi encontrado(a)!");
                }

                return Ok(processBadge);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo processo de badge.
        /// </summary>
        /// <param name="processBadge">O processo de badge a ser criado.</param>
        /// <returns>O processo de badge criado.</returns>
        /// <response code="201">Processo de badge criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<ProcessBadge>> CreateProcessBadge([FromBody] ProcessBadge processBadge)
        {
            try
            {
                if (processBadge == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdProcessBadge = await _processBadgeRepository.AddProcessBadge(processBadge);
                return CreatedAtAction(nameof(GetProcessBadgeById), new
                {
                    processBadgeId = createdProcessBadge.id_process_badge
                }, createdProcessBadge);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um processo de badge existente.
        /// </summary>
        /// <param name="processBadgeId">O ID do processo de badge a ser atualizado.</param>
        /// <param name="processBadge">Os novos dados do processo de badge.</param>
        /// <returns>O processo de badge atualizado.</returns>
        /// <response code="200">Processo de badge atualizado com sucesso.</response>
        /// <response code="404">Processo de badge não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar os dados no banco de dados.</response>
        [HttpPut("{processBadgeId:int}")]
        public async Task<ActionResult<ProcessBadge>> UpdateProcessBadge(int processBadgeId, [FromBody] ProcessBadge processBadge)
        {
            try
            {
                if (processBadge == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                processBadge.id_process_badge = processBadgeId;
                var updatedProcessBadge = await _processBadgeRepository.UpdateProcessBadge(processBadge);

                if (updatedProcessBadge == null)
                {
                    return NotFound($"Processo da Badge com o ID: {processBadgeId}, não foi encontrado(a)!");
                }

                return Ok(updatedProcessBadge);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um processo de badge pelo ID.
        /// </summary>
        /// <param name="processBadgeId">O ID do processo de badge a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Processo de badge deletado com sucesso.</response>
        /// <response code="404">Processo de badge não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{processBadgeId:int}")]
        public async Task<ActionResult<ProcessBadge>> DeleteProcessBadge(int processBadgeId)
        {
            try
            {
                var deletedProcessBadge = await _processBadgeRepository.GetProcessBadgeById(processBadgeId);

                if (deletedProcessBadge == null)
                {
                    return NotFound($"Processo da Badge com o ID: {processBadgeId}, não foi encontrado(a)!");
                }

                _processBadgeRepository.DeleteProcessBadge(processBadgeId);
                return Ok("Processo da Badge, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }
    }
}