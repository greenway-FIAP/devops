using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/sector")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorRepository _sectorRepository;

        public SectorController(ISectorRepository sectorRepository)
        {
            this._sectorRepository = sectorRepository;
        }

        /// <summary>
        /// Obtém todos os setores.
        /// </summary>
        /// <returns>Uma lista de setores.</returns>
        /// <response code="200">Retorna a lista de setores.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sector>>> GetSectors()
        {
            try
            {
                var sectors = await _sectorRepository.GetSectors();
                return Ok(sectors);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um setor pelo ID.
        /// </summary>
        /// <param name="sectorId">O ID do setor.</param>
        /// <returns>O setor correspondente ao ID.</returns>
        /// <response code="200">Retorna o setor encontrado.</response>
        /// <response code="404">Setor não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{sectorId:int}")]
        public async Task<ActionResult<Sector>> GetSectorById(int sectorId)
        {
            try
            {
                var sector = await _sectorRepository.GetSectorById(sectorId);

                if (sector == null)
                {
                    return NotFound($"Setor com o ID: {sectorId}, não encontrado(a)!");
                }

                return Ok(sector);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo setor.
        /// </summary>
        /// <param name="sector">O setor a ser criado.</param>
        /// <returns>O setor criado.</returns>
        /// <response code="201">Setor criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<Sector>> CreateSector([FromBody] Sector sector)
        {
            try
            {
                if (sector == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdSector = await _sectorRepository.AddSector(sector);
                return CreatedAtAction(nameof(GetSectorById), new
                {
                    sectorId = createdSector.id_sector
                }, createdSector);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um setor existente.
        /// </summary>
        /// <param name="sectorId">O ID do setor a ser atualizado.</param>
        /// <param name="sector">Os novos dados do setor.</param>
        /// <returns>O setor atualizado.</returns>
        /// <response code="200">Setor atualizado com sucesso.</response>
        /// <response code="404">Setor não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar dados no banco de dados.</response>
        [HttpPut("{sectorId:int}")]
        public async Task<ActionResult<Sector>> UpdateSector(int sectorId, [FromBody] Sector sector)
        {
            try
            {
                if (sector == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                sector.id_sector = sectorId;
                var updatedSector = await _sectorRepository.UpdateSector(sector);

                if (updatedSector == null)
                {
                    return NotFound($"Setor com o ID: {sectorId}, não encontrado(a)!");
                }

                return Ok(updatedSector);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um setor pelo ID.
        /// </summary>
        /// <param name="sectorId">O ID do setor a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Setor deletado com sucesso.</response>
        /// <response code="404">Setor não encontrado.</response>
        /// <response code="500">Erro ao deletar dados no banco de dados.</response>
        [HttpDelete("{sectorId:int}")]
        public async Task<ActionResult<Sector>> DeleteSector(int sectorId)
        {
            try
            {
                var deletedSector = await _sectorRepository.GetSectorById(sectorId);
                if (deletedSector == null)
                {
                    return NotFound($"Setor com o ID: {sectorId}, não encontrado(a)!");
                }

                _sectorRepository.DeleteSector(sectorId);
                return Ok("Setor, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}