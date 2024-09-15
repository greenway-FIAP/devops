using ApiGreenway.Models;
using ApiGreenway.Repository;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/address")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressRepository _addressRepository;

    public AddressController(IAddressRepository addressRepository)
    {
        this._addressRepository = addressRepository;
    }

    /// <summary>
    /// Obtém todos os endereços.
    /// </summary>
    /// <returns>Uma lista de endereços.</returns>
    /// <response code="200">Retorna a lista de endereços</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
    {
        try
        {
            var addresses = await _addressRepository.GetAddresses();
            return Ok(addresses);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém um endereço pelo ID.
    /// </summary>
    /// <param name="addressId">O ID do endereço.</param>
    /// <returns>Um endereço específico.</returns>
    /// <response code="200">Retorna o endereço com o ID fornecido</response>
    /// <response code="404">Se o endereço não for encontrado</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{addressId:int}")]
    public async Task<ActionResult<Address>> GetAddressById(int addressId)
    {
        try
        {
            var address = await _addressRepository.GetAddressById(addressId);
            if (address == null)
            {
                return NotFound($"Endereço com o ID: {addressId}, não foi encontrado(a)!");
            }

            return Ok(address);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria um novo endereço.
    /// </summary>
    /// <param name="address">Os dados do endereço a ser criado.</param>
    /// <returns>O endereço criado.</returns>
    /// <response code="201">Retorna o endereço criado</response>
    /// <response code="400">Se os dados do endereço forem inválidos</response>
    /// <response code="500">Erro ao adicionar dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<Address>> CreateAddress([FromBody] Address address)
    {
        try
        {
            if (address == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdAddress = await _addressRepository.AddAddress(address);
            return CreatedAtAction(nameof(GetAddressById), new
            {
                addressId = createdAddress.id_address
            }, createdAddress);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza um endereço existente.
    /// </summary>
    /// <param name="addressId">O ID do endereço a ser atualizado.</param>
    /// <param name="address">Os novos dados do endereço.</param>
    /// <returns>O endereço atualizado.</returns>
    /// <response code="200">Retorna o endereço atualizado</response>
    /// <response code="404">Se o endereço não for encontrado</response>
    /// <response code="400">Se os dados do endereço forem inválidos</response>
    /// <response code="500">Erro ao atualizar os dados no Banco de Dados</response>
    [HttpPut("{addressId:int}")]
    public async Task<ActionResult<Address>> UpdateAddress(int addressId, [FromBody] Address address)
    {
        try
        {
            if (address == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            address.id_address = addressId;
            var updatedAddress = await _addressRepository.UpdateAddress(address);

            if (updatedAddress == null)
            {
                return NotFound($"Endereço com o ID: {addressId}, não foi encontrado(a)!");
            }

            return Ok(updatedAddress);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta um endereço pelo ID.
    /// </summary>
    /// <param name="addressId">O ID do endereço a ser deletado.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se o endereço não for encontrado</response>
    /// <response code="500">Erro ao deletar os dados do Banco de Dados</response>
    [HttpDelete("{addressId:int}")]
    public async Task<ActionResult<Address>> DeleteAddress(int addressId)
    {
        try
        {
            var deletedAddress = await _addressRepository.GetAddressById(addressId);

            if (deletedAddress == null)
            {
                return NotFound($"Endereço com o ID: {addressId}, não foi encontrado(a)!");
            }

            _addressRepository.DeleteAddress(addressId);

            return Ok("Endereço, foi deletado(a) com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados do Banco de Dados");
        }
    }
}