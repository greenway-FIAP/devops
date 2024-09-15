using ApiGreenway.Models;
using ApiGreenway.Repository;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers;

[Route("api/badge-level")]
[ApiController]
public class BadgeLevelController : ControllerBase
{
    private readonly IBadgeLevelRepository _badgeLevelRepository;

    public BadgeLevelController(IBadgeLevelRepository badgeLevelRepository)
    {
        this._badgeLevelRepository = badgeLevelRepository;
    }

    /// <summary>
    /// Obtém todos os níveis de badge.
    /// </summary>
    /// <returns>Uma lista de níveis de badge.</returns>
    /// <response code="200">Retorna a lista de níveis de badge</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BadgeLevel>>> GetBadgeLevels()
    {
        try
        {
            var badgeLevels = await _badgeLevelRepository.GetBadgeLevels();
            return Ok(badgeLevels);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Obtém um nível de badge pelo ID.
    /// </summary>
    /// <param name="badgeLevelId">O ID do nível de badge.</param>
    /// <returns>Um nível de badge específico.</returns>
    /// <response code="200">Retorna o nível de badge com o ID fornecido</response>
    /// <response code="404">Se o nível de badge não for encontrado</response>
    /// <response code="500">Erro ao recuperar os dados do Banco de Dados</response>
    [HttpGet("{badgeLevelId:int}")]
    public async Task<ActionResult<BadgeLevel>> GetBadgeLevelById(int badgeLevelId)
    {
        try
        {
            var badgeLevel = await _badgeLevelRepository.GetBadgeLevelById(badgeLevelId);
            if (badgeLevel == null)
            {
                return NotFound($"Nível da Badge com o ID: {badgeLevelId}, não foi encontrado");
            }

            return Ok(badgeLevel);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
        }
    }

    /// <summary>
    /// Cria um novo nível de badge.
    /// </summary>
    /// <param name="badgeLevel">Os dados do nível de badge a ser criado.</param>
    /// <returns>O nível de badge criado.</returns>
    /// <response code="201">Retorna o nível de badge criado</response>
    /// <response code="400">Se os dados do nível de badge forem inválidos</response>
    /// <response code="500">Erro ao adicionar dados no Banco de Dados</response>
    [HttpPost]
    public async Task<ActionResult<BadgeLevel>> CreateBadgeLevel([FromBody] BadgeLevel badgeLevel)
    {
        try
        {
            if (badgeLevel == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            var createdBadgeLevel = await _badgeLevelRepository.AddBadgeLevel(badgeLevel);
            return CreatedAtAction(nameof(GetBadgeLevelById), new
            {
                badgeLevelId = createdBadgeLevel.id_badge_level
            }, createdBadgeLevel);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Atualiza um nível de badge existente.
    /// </summary>
    /// <param name="badgeLevelId">O ID do nível de badge a ser atualizado.</param>
    /// <param name="badgeLevel">Os novos dados do nível de badge.</param>
    /// <returns>O nível de badge atualizado.</returns>
    /// <response code="200">Retorna o nível de badge atualizado</response>
    /// <response code="404">Se o nível de badge não for encontrado</response>
    /// <response code="400">Se os dados do nível de badge forem inválidos</response>
    /// <response code="500">Erro ao atualizar os dados no Banco de Dados</response>
    [HttpPut("{badgeLevelId:int}")]
    public async Task<ActionResult<BadgeLevel>> UpdateBadgeLevel(int badgeLevelId, [FromBody] BadgeLevel badgeLevel)
    {
        try
        {
            if (badgeLevel == null)
            {
                return BadRequest("Alguns dados estão inválidos, verifique!!");
            }

            badgeLevel.id_badge_level = badgeLevelId;
            var updatedBadgeLevel = await _badgeLevelRepository.UpdateBadgeLevel(badgeLevel);
            if (updatedBadgeLevel == null)
            {
                return NotFound($"Nível da Badge com o ID: {badgeLevelId}, não foi encontrado");
            }

            return Ok(updatedBadgeLevel);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
        }
    }

    /// <summary>
    /// Deleta um nível de badge pelo ID.
    /// </summary>
    /// <param name="badgeLevelId">O ID do nível de badge a ser deletado.</param>
    /// <returns>Mensagem de confirmação da exclusão.</returns>
    /// <response code="200">Retorna uma mensagem de sucesso</response>
    /// <response code="404">Se o nível de badge não for encontrado</response>
    /// <response code="500">Erro ao deletar os dados do Banco de Dados</response>
    [HttpDelete("{badgeLevelId:int}")]
    public async Task<ActionResult<BadgeLevel>> DeleteBadgeLevel(int badgeLevelId)
    {
        try
        {
            var deletedBadgeLevel = await _badgeLevelRepository.GetBadgeLevelById(badgeLevelId);
            if (deletedBadgeLevel == null)
            {
                return NotFound($"Nível da Badge com o ID: {badgeLevelId}, não foi encontrado");
            }

            _badgeLevelRepository.DeleteBadgeLevel(badgeLevelId);

            return Ok("Nível da Badge, foi deletado(a) com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados do Banco de Dados");
        }
    }
}