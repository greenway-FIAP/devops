using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        /// <response code="200">Retorna a lista de produtos.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um produto pelo ID.
        /// </summary>
        /// <param name="productId">O ID do produto.</param>
        /// <returns>O produto correspondente ao ID.</returns>
        /// <response code="200">Retorna o produto encontrado.</response>
        /// <response code="404">Produto não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetProductById(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductById(productId);
                if (product == null)
                {
                    return NotFound($"Produto com o ID: {productId}, não foi encontrado(a)!");
                }

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="product">O produto a ser criado.</param>
        /// <returns>O produto criado.</returns>
        /// <response code="201">Produto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdProduct = await _productRepository.AddProduct(product);
                return CreatedAtAction(nameof(GetProductById), new
                {
                    productId = createdProduct.id_product
                }, createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="productId">O ID do produto a ser atualizado.</param>
        /// <param name="product">Os novos dados do produto.</param>
        /// <returns>O produto atualizado.</returns>
        /// <response code="200">Produto atualizado com sucesso.</response>
        /// <response code="404">Produto não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{productId:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int productId, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                product.id_product = productId;
                var updatedProduct = await _productRepository.UpdateProduct(product);

                if (updatedProduct == null)
                {
                    return NotFound($"Produto com o ID: {productId}, não foi encontrado(a)!");
                }

                return Ok(updatedProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um produto pelo ID.
        /// </summary>
        /// <param name="productId">O ID do produto a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Produto deletado com sucesso.</response>
        /// <response code="404">Produto não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{productId:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int productId)
        {
            try
            {
                var deletedProduct = await _productRepository.GetProductById(productId);
                if (deletedProduct == null)
                {
                    return NotFound($"Produto com o ID: {productId}, não foi encontrado(a)!");
                }

                _productRepository.DeleteProduct(productId);
                return Ok("Produto, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}