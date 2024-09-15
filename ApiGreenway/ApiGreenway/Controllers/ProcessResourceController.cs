using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/process-resource")]
    [ApiController]
    public class ProcessResourceController : ControllerBase
    {
        private readonly IProcessResourceRepository _processResourceRepository;

        public ProcessResourceController(IProcessResourceRepository processResourceRepository)
        {
            this._processResourceRepository = processResourceRepository;
        }

        /// <summary>
        /// Obtém todos os recursos de processo.
        /// </summary>
        /// <returns>Uma lista de recursos de processo.</returns>
        /// <response code="200">Retorna a lista de recursos de processo.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessResource>>> GetProcessResources()
        {
            try
            {
                var processResources = await _processResourceRepository.GetProcessResources();
                return Ok(processResources);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um recurso de processo pelo ID.
        /// </summary>
        /// <param name="processResourceId">O ID do recurso de processo.</param>
        /// <returns>O recurso de processo correspondente ao ID.</returns>
        /// <response code="200">Retorna o recurso de processo encontrado.</response>
        /// <response code="404">Recurso de processo não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{processResourceId:int}")]
        public async Task<ActionResult<ProcessResource>> GetProcessResourceById(int processResourceId)
        {
            try
            {
                var processResource = await _processResourceRepository.GetProcessResourceById(processResourceId);
                if (processResource == null)
                {
                    return NotFound($"Processo do Recurso com o Id = {processResourceId} não foi encontrado(a)!");
                }

                return Ok(processResource);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo recurso de processo.
        /// </summary>
        /// <param name="processResource">O recurso de processo a ser criado.</param>
        /// <returns>O recurso de processo criado.</returns>
        /// <response code="201">Recurso de processo criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<ProcessResource>> CreateProcessResource([FromBody] ProcessResource processResource)
        {
            try
            {
                if (processResource == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdProcessResource = await _processResourceRepository.AddProcessResource(processResource);
                return CreatedAtAction(nameof(GetProcessResourceById), new
                {
                    processResourceId = createdProcessResource.id_process_resource
                }, createdProcessResource);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um recurso de processo existente.
        /// </summary>
        /// <param name="processResourceId">O ID do recurso de processo a ser atualizado.</param>
        /// <param name="processResource">Os novos dados do recurso de processo.</param>
        /// <returns>O recurso de processo atualizado.</returns>
        /// <response code="200">Recurso de processo atualizado com sucesso.</response>
        /// <response code="404">Recurso de processo não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{processResourceId:int}")]
        public async Task<ActionResult<ProcessResource>> UpdateProcessResource(int processResourceId, [FromBody] ProcessResource processResource)
        {
            try
            {
                if (processResource == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                processResource.id_process_resource = processResourceId;
                var updatedProcessResource = await _processResourceRepository.UpdateProcessResource(processResource);

                if (updatedProcessResource == null)
                {
                    return NotFound($"Processo do Recurso com o Id = {processResourceId} não foi encontrado(a)!");
                }

                return Ok(updatedProcessResource);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um recurso de processo pelo ID.
        /// </summary>
        /// <param name="processResourceId">O ID do recurso de processo a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Recurso de processo deletado com sucesso.</response>
        /// <response code="404">Recurso de processo não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{processResourceId:int}")]
        public async Task<ActionResult<ProcessResource>> DeleteProcessResource(int processResourceId)
        {
            try
            {
                var deletedProcessResource = await _processResourceRepository.GetProcessResourceById(processResourceId);

                if (deletedProcessResource == null)
                {
                    return NotFound($"Processo do Recurso com o Id = {processResourceId} não foi encontrado(a)!");
                }

                _processResourceRepository.DeleteProcessResource(processResourceId);
                return Ok("Processo do Recurso, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}