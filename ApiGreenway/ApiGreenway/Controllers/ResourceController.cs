using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/resource")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceController(IResourceRepository resourceRepository)
        {
            this._resourceRepository = resourceRepository;
        }

        /// <summary>
        /// Obtém todos os recursos.
        /// </summary>
        /// <returns>Uma lista de recursos.</returns>
        /// <response code="200">Retorna a lista de recursos.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
        {
            try
            {
                var resources = await _resourceRepository.GetResources();
                return Ok(resources);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um recurso pelo ID.
        /// </summary>
        /// <param name="resourceId">O ID do recurso.</param>
        /// <returns>O recurso correspondente ao ID.</returns>
        /// <response code="200">Retorna o recurso encontrado.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{resourceId:int}")]
        public async Task<ActionResult<Resource>> GetResourceById(int resourceId)
        {
            try
            {
                var resource = await _resourceRepository.GetResourceById(resourceId);

                if (resource == null)
                {
                    return NotFound($"Recurso com Id = {resourceId} não foi encontrado");
                }

                return Ok(resource);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo recurso.
        /// </summary>
        /// <param name="resource">O recurso a ser criado.</param>
        /// <returns>O recurso criado.</returns>
        /// <response code="201">Recurso criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<Resource>> CreateResource([FromBody] Resource resource)
        {
            try
            {
                if (resource == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdResource = await _resourceRepository.AddResource(resource);
                return CreatedAtAction(nameof(GetResourceById), new
                {
                    resourceId = createdResource.id_resource
                }, createdResource);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um recurso existente.
        /// </summary>
        /// <param name="resourceId">O ID do recurso a ser atualizado.</param>
        /// <param name="resource">Os novos dados do recurso.</param>
        /// <returns>O recurso atualizado.</returns>
        /// <response code="200">Recurso atualizado com sucesso.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{resourceId:int}")]
        public async Task<ActionResult<Resource>> UpdateResource(int resourceId, [FromBody] Resource resource)
        {
            try
            {
                if (resource == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                resource.id_resource = resourceId;
                var updatedResource = await _resourceRepository.UpdateResource(resource);

                if (updatedResource == null)
                {
                    return NotFound($"Recurso com Id = {resourceId} não foi encontrado");
                }

                return Ok(updatedResource);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um recurso pelo ID.
        /// </summary>
        /// <param name="resourceId">O ID do recurso a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Recurso deletado com sucesso.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{resourceId:int}")]
        public async Task<ActionResult<Resource>> DeleteResource(int resourceId)
        {
            try
            {
                var deletedResource = await _resourceRepository.GetResourceById(resourceId);
                if (deletedResource == null)
                {
                    return NotFound($"Recurso com Id = {resourceId} não foi encontrado");
                }

                _resourceRepository.DeleteResource(resourceId);
                return Ok("Recurso, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}