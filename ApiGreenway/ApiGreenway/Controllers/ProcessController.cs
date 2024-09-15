using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/process")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessRepository _processRepository;

        public ProcessController(IProcessRepository processRepository)
        {
            this._processRepository = processRepository;
        }

        /// <summary>
        /// Obtém todos os processos.
        /// </summary>
        /// <returns>Uma lista de processos.</returns>
        /// <response code="200">Retorna a lista de processos.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Process>>> GetProcesses()
        {
            try
            {
                var processes = await _processRepository.GetProcesses();
                return Ok(processes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um processo pelo ID.
        /// </summary>
        /// <param name="processId">O ID do processo.</param>
        /// <returns>O processo correspondente ao ID.</returns>
        /// <response code="200">Retorna o processo encontrado.</response>
        /// <response code="404">Processo não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{processId:int}")]
        public async Task<ActionResult<Process>> GetProcessById(int processId)
        {
            try
            {
                var process = await _processRepository.GetProcessById(processId);
                if (process == null)
                {
                    return NotFound($"Processo com o ID: {processId}, não foi encontrado(a)!");
                }

                return Ok(process);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo processo.
        /// </summary>
        /// <param name="process">O processo a ser criado.</param>
        /// <returns>O processo criado.</returns>
        /// <response code="201">Processo criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<Process>> CreateProcess([FromBody] Process process)
        {
            try
            {
                if (process == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdProcess = await _processRepository.AddProcess(process);
                return CreatedAtAction(nameof(GetProcessById), new
                {
                    processId = createdProcess.id_process
                }, createdProcess);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um processo existente.
        /// </summary>
        /// <param name="processId">O ID do processo a ser atualizado.</param>
        /// <param name="process">Os novos dados do processo.</param>
        /// <returns>O processo atualizado.</returns>
        /// <response code="200">Processo atualizado com sucesso.</response>
        /// <response code="404">Processo não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar os dados no banco de dados.</response>
        [HttpPut("{processId:int}")]
        public async Task<ActionResult<Process>> UpdateProcess(int processId, [FromBody] Process process)
        {
            try
            {
                if (process == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                process.id_process = processId;
                var updatedProcess = await _processRepository.UpdateProcess(process);

                if (updatedProcess == null)
                {
                    return NotFound($"Processo com o ID: {processId}, não foi encontrado(a)!");
                }

                return Ok(updatedProcess);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um processo pelo ID.
        /// </summary>
        /// <param name="processId">O ID do processo a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Processo deletado com sucesso.</response>
        /// <response code="404">Processo não encontrado.</response>
        /// <response code="500">Erro ao deletar os dados do banco de dados.</response>
        [HttpDelete("{processId:int}")]
        public async Task<ActionResult<Process>> DeleteProcess(int processId)
        {
            try
            {
                var deletedProcess = await _processRepository.GetProcessById(processId);

                if (deletedProcess == null)
                {
                    return NotFound($"Processo com o ID: {processId}, não foi encontrado(a)!");
                }

                _processRepository.DeleteProcess(processId);
                return Ok("Processo, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados do Banco de Dados");
            }
        }
    }
}