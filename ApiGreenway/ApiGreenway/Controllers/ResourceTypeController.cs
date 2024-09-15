using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using ResourceType = ApiGreenway.Models.ResourceType;

namespace ApiGreenway.Controllers
{
    [Route("api/resource-type")]
    [ApiController]
    public class ResourceTypeController : ControllerBase
    {
        private readonly IResourceTypeRepository _resourceTypeRepository;

        public ResourceTypeController(IResourceTypeRepository resourceTypeRepository)
        {
            this._resourceTypeRepository = resourceTypeRepository;
        }

        /// <summary>
        /// Obtém todos os tipos de recursos.
        /// </summary>
        /// <returns>Uma lista de tipos de recursos.</returns>
        /// <response code="200">Retorna a lista de tipos de recursos.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceType>>> GetResourceTypes()
        {
            try
            {
                var resourceTypes = await _resourceTypeRepository.GetResourceTypes();
                return Ok(resourceTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um tipo de recurso pelo ID.
        /// </summary>
        /// <param name="resourceTypeId">O ID do tipo de recurso.</param>
        /// <returns>O tipo de recurso correspondente ao ID.</returns>
        /// <response code="200">Retorna o tipo de recurso encontrado.</response>
        /// <response code="404">Tipo de recurso não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{resourceTypeId:int}")]
        public async Task<ActionResult<ResourceType>> GetResourceTypeById(int resourceTypeId)
        {
            try
            {
                var resourceType = await _resourceTypeRepository.GetResourceTypeById(resourceTypeId);

                if (resourceType == null)
                {
                    return NotFound($"Tipo de Recurso com o ID: {resourceTypeId}, não foi encontrado(a)!");
                }

                return Ok(resourceType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo tipo de recurso.
        /// </summary>
        /// <param name="resourceType">O tipo de recurso a ser criado.</param>
        /// <returns>O tipo de recurso criado.</returns>
        /// <response code="201">Tipo de recurso criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<ResourceType>> CreateResourceType([FromBody] ResourceType resourceType)
        {
            try
            {
                if (resourceType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdResourceType = await _resourceTypeRepository.AddResourceType(resourceType);
                return CreatedAtAction(nameof(GetResourceTypeById), new
                {
                    resourceTypeId = createdResourceType.id_resource_type
                }, createdResourceType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um tipo de recurso existente.
        /// </summary>
        /// <param name="resourceTypeId">O ID do tipo de recurso a ser atualizado.</param>
        /// <param name="resourceType">Os novos dados do tipo de recurso.</param>
        /// <returns>O tipo de recurso atualizado.</returns>
        /// <response code="200">Tipo de recurso atualizado com sucesso.</response>
        /// <response code="404">Tipo de recurso não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{resourceTypeId:int}")]
        public async Task<ActionResult<ResourceType>> UpdateResourceType(int resourceTypeId, [FromBody] ResourceType resourceType)
        {
            try
            {
                if (resourceType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                resourceType.id_resource_type = resourceTypeId;
                var updatedResourceType = await _resourceTypeRepository.UpdateResourceType(resourceType);

                if (updatedResourceType == null)
                {
                    return NotFound($"Tipo de Recurso com o ID: {resourceTypeId}, não foi encontrado(a)!");
                }

                return Ok(updatedResourceType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um tipo de recurso pelo ID.
        /// </summary>
        /// <param name="resourceTypeId">O ID do tipo de recurso a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Tipo de recurso deletado com sucesso.</response>
        /// <response code="404">Tipo de recurso não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{resourceTypeId:int}")]
        public async Task<ActionResult<ResourceType>> DeleteResourceType(int resourceTypeId)
        {
            try
            {
                var deletedResourceType = await _resourceTypeRepository.GetResourceTypeById(resourceTypeId);
                if (deletedResourceType == null)
                {
                    return NotFound($"Tipo de Recurso com o ID: {resourceTypeId}, não foi encontrado(a)!");
                }

                _resourceTypeRepository.DeleteResourceType(resourceTypeId);
                return Ok("Tipo de Recurso, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}