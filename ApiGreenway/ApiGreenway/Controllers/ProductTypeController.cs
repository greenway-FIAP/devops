using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/product-type")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeController(IProductTypeRepository productTypeRepository)
        {
            this._productTypeRepository = productTypeRepository;
        }

        /// <summary>
        /// Obtém todos os tipos de produtos.
        /// </summary>
        /// <returns>Uma lista de tipos de produtos.</returns>
        /// <response code="200">Retorna a lista de tipos de produtos.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            try
            {
                var productTypes = await _productTypeRepository.GetProductTypes();
                return Ok(productTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um tipo de produto pelo ID.
        /// </summary>
        /// <param name="productTypeId">O ID do tipo de produto.</param>
        /// <returns>O tipo de produto correspondente ao ID.</returns>
        /// <response code="200">Retorna o tipo de produto encontrado.</response>
        /// <response code="404">Tipo de produto não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{productTypeId:int}")]
        public async Task<ActionResult<ProductType>> GetProductTypeById(int productTypeId)
        {
            try
            {
                var productType = await _productTypeRepository.GetProductTypeById(productTypeId);
                if (productType == null)
                {
                    return NotFound($"Tipo de Produto com o ID: {productTypeId}, não foi encontrado(a)!");
                }

                return Ok(productType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo tipo de produto.
        /// </summary>
        /// <param name="productType">O tipo de produto a ser criado.</param>
        /// <returns>O tipo de produto criado.</returns>
        /// <response code="201">Tipo de produto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<ProductType>> CreateProductType([FromBody] ProductType productType)
        {
            try
            {
                if (productType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdProductType = await _productTypeRepository.AddProductType(productType);
                return CreatedAtAction(nameof(GetProductTypeById), new
                {
                    productTypeId = createdProductType.id_product_type
                }, createdProductType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um tipo de produto existente.
        /// </summary>
        /// <param name="productTypeId">O ID do tipo de produto a ser atualizado.</param>
        /// <param name="productType">Os novos dados do tipo de produto.</param>
        /// <returns>O tipo de produto atualizado.</returns>
        /// <response code="200">Tipo de produto atualizado com sucesso.</response>
        /// <response code="404">Tipo de produto não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{productTypeId:int}")]
        public async Task<ActionResult<ProductType>> UpdateProductType(int productTypeId, [FromBody] ProductType productType)
        {
            try
            {
                if (productType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                productType.id_product_type = productTypeId;
                var updatedProductType = await _productTypeRepository.UpdateProductType(productType);

                if (updatedProductType == null)
                {
                    return NotFound($"Tipo de Produto com o ID: {productTypeId}, não foi encontrado(a)!");
                }

                return Ok(updatedProductType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um tipo de produto pelo ID.
        /// </summary>
        /// <param name="productTypeId">O ID do tipo de produto a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Tipo de produto deletado com sucesso.</response>
        /// <response code="404">Tipo de produto não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{productTypeId:int}")]
        public async Task<ActionResult<ProductType>> DeleteProductType(int productTypeId)
        {
            try
            {
                var deletedProductType = await _productTypeRepository.GetProductTypeById(productTypeId);
                if (deletedProductType == null)
                {
                    return NotFound($"Tipo de Produto com o ID: {productTypeId}, não foi encontrado(a)!");
                }

                _productTypeRepository.DeleteProductType(productTypeId);
                return Ok("Tipo de Produto, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}