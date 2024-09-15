using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGreenway.Controllers
{
    [Route("api/user-type")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeController(IUserTypeRepository userTypeRepository)
        {
            this._userTypeRepository = userTypeRepository;
        }

        /// <summary>
        /// Obtém todos os tipos de usuário.
        /// </summary>
        /// <returns>Uma lista de tipos de usuário.</returns>
        /// <response code="200">Retorna a lista de tipos de usuário.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
        {
            try
            {
                var userTypes = await _userTypeRepository.GetUserTypes();
                return Ok(userTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um tipo de usuário pelo ID.
        /// </summary>
        /// <param name="userTypeId">O ID do tipo de usuário.</param>
        /// <returns>O tipo de usuário correspondente ao ID.</returns>
        /// <response code="200">Retorna o tipo de usuário encontrado.</response>
        /// <response code="404">Tipo de usuário não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{userTypeId:int}")]
        public async Task<ActionResult<UserType>> GetUserTypeById(int userTypeId)
        {
            try
            {
                var userType = await _userTypeRepository.GetUserTypeById(userTypeId);

                if (userType == null)
                {
                    return NotFound($"Tipo de Usuário com o ID: {userTypeId}, não foi encontrado(a)!");
                }

                return Ok(userType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Cria um novo tipo de usuário.
        /// </summary>
        /// <param name="userType">O tipo de usuário a ser criado.</param>
        /// <returns>O tipo de usuário criado.</returns>
        /// <response code="201">Tipo de usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost]
        public async Task<ActionResult<UserType>> CreateUserType([FromBody] UserType userType)
        {
            try
            {
                if (userType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdUserType = await _userTypeRepository.AddUserType(userType);
                return CreatedAtAction(nameof(GetUserTypeById), new
                {
                    userTypeId = createdUserType.id_user_type
                }, createdUserType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Atualiza um tipo de usuário existente.
        /// </summary>
        /// <param name="userTypeId">O ID do tipo de usuário a ser atualizado.</param>
        /// <param name="userType">Os novos dados do tipo de usuário.</param>
        /// <returns>O tipo de usuário atualizado.</returns>
        /// <response code="200">Tipo de usuário atualizado com sucesso.</response>
        /// <response code="404">Tipo de usuário não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar os dados no banco de dados.</response>
        [HttpPut("{userTypeId:int}")]
        public async Task<ActionResult<UserType>> UpdateUserType(int userTypeId, [FromBody] UserType userType)
        {
            try
            {
                if (userType == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                userType.id_user_type = userTypeId;
                var updatedUserType = await _userTypeRepository.UpdateUserType(userType);

                if (updatedUserType == null)
                {
                    return NotFound($"Tipo de Usuário com o ID: {userTypeId}, não foi encontrado(a)!");
                }

                return Ok(updatedUserType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um tipo de usuário pelo ID.
        /// </summary>
        /// <param name="userTypeId">O ID do tipo de usuário a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Tipo de usuário deletado com sucesso.</response>
        /// <response code="404">Tipo de usuário não encontrado.</response>
        /// <response code="500">Erro ao deletar os dados do banco de dados.</response>
        [HttpDelete("{userTypeId:int}")]
        public async Task<ActionResult<UserType>> DeleteUserType(int userTypeId)
        {
            try
            {
                var deletedUserType = await _userTypeRepository.GetUserTypeById(userTypeId);

                if (deletedUserType == null)
                {
                    return NotFound($"Tipo de Usuário com o ID: {userTypeId}, não foi encontrado(a)!");
                }

                _userTypeRepository.DeleteUserType(userTypeId);
                return Ok("Tipo de Usuário, foi deletado(a) com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no Banco de Dados");
            }
        }
    }
}