using ApiGreenway.Models;
using ApiGreenway.Repository;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/badge")]
[ApiController]
public class BadgeController : ControllerBase
{
    private readonly IBadgeRepository _badgeRepository;

    public BadgeController(IBadgeRepository badgeRepository)
    {
        this._badgeRepository = badgeRepository;
    }

    /// <summary>
    /// Obtém todos os badges.
    /// </summary>
    /// <returns>Uma lista de badges.</returns>
    /// <response code="200">Retorna a lista de badges</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Badge>>> GetBadges()
    {
        try
        {
            var badges = await _badgeRepository.GetBadges();
            return Ok(badges);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém um badge pelo ID.
    /// </summary>
    /// <param name="badgeId">O ID do badge.</param>
    /// <returns>Um badge específico.</returns>
    /// <response code="200">Retorna o badge com o ID fornecido</response>
    /// <response code="404">Se o badge não for encontrado</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{badgeId:int}")]
    public async Task<ActionResult<Badge>> GetBadgeById(int badgeId)
    {
        try
        {
            var badge = await _badgeRepository.GetBadgeById(badgeId);
            if (badge == null)
            {
                return NotFound($"Badge com o ID: {badgeId}, não foi encontrado(a)!");
            }

            return Ok(badge);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria um novo badge.
    /// </summary>
    /// <param name="badge">Os dados do badge a ser criado.</param>
    /// <returns>O badge criado.</returns>
    /// <response code="201">Retorna o badge criado</response>
    /// <response code="400">Se os dados do badge forem inválidos</response>
    /// <response code="500">Erro ao adicionar dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<Badge>> CreateBadge([FromBody] Badge badge)
    {
        try
        {
            if (badge == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdBadge = await _badgeRepository.AddBadge(badge);
            return CreatedAtAction(nameof(GetBadgeById), new
            {
                badgeId = createdBadge.id_badge
            }, createdBadge);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza um badge existente.
    /// </summary>
    /// <param name="badgeId">O ID do badge a ser atualizado.</param>
    /// <param name="badge">Os novos dados do badge.</param>
    /// <returns>O badge atualizado.</returns>
    /// <response code="200">Retorna o badge atualizado</response>
    /// <response code="404">Se o badge não for encontrado</response>
    /// <response code="400">Se os dados do badge forem inválidos</response>
    /// <response code="500">Erro ao atualizar os dados no Banco de Dados</response>
    [HttpPut("{badgeId:int}")]
    public async Task<ActionResult<Badge>> UpdateBadge(int badgeId, [FromBody] Badge badge)
    {
        try
        {
            if (badge == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            badge.id_badge = badgeId;
            var updatedBadge = await _badgeRepository.UpdateBadge(badge);

            if (updatedBadge == null)
            {
                return NotFound($"Badge com o ID: {badgeId}, não foi encontrado(a)!");
            }

            return Ok(updatedBadge);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta um badge pelo ID.
    /// </summary>
    /// <param name="badgeId">O ID do badge a ser deletado.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se o badge não for encontrado</response>
    /// <response code="500">Erro ao deletar os dados do Banco de Dados</response>
    [HttpDelete("{badgeId:int}")]
    public async Task<ActionResult<Badge>> DeleteBadge(int badgeId)
    {
        try
        {
            var deletedBadge = await _badgeRepository.GetBadgeById(badgeId);

            if (deletedBadge == null)
            {
                return NotFound($"Badge com o ID: {badgeId}, não foi encontrado(a)!");
            }

            _badgeRepository.DeleteBadge(badgeId);

            return Ok("Badge, foi deletado(a) com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados do Banco de Dados");
        }
    }
}