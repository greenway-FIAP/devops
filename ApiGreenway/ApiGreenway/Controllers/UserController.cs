using ApiGreenway.Models;
using ApiGreenway.Models.Dtos;
using ApiGreenway.Repository.Interface;
using ApiGreenway.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiGreenway.Controllers
{
    /// <summary>
    /// Controlador para a entidade User.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        /// <summary>
        /// Construtor para a classe UserController.
        /// </summary>
        /// <param name="userRepository">Repositório de usuários.</param>
        /// <param name="authService">Serviço de autenticação.</param>
        public UserController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <summary>
        /// Faz login de um usuário.
        /// </summary>
        /// <param name="request">Os dados do usuário a ser autenticado.</param>
        /// <returns>Um token JWT se o login for bem-sucedido.</returns>
        /// <response code="200">Login bem-sucedido.</response>
        /// <response code="400">E-mail ou senha inválidos.</response>
        /// <response code="500">Erro ao realizar login.</response>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.ds_email) || string.IsNullOrEmpty(request.ds_password))
            {
                return BadRequest("E-mail e senha são obrigatórios.");
            }

            try
            {
                string token = await _authService.LoginAsync(request);
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("E-mail ou senha inválidos.");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar login: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        /// <response code="200">Retorna a lista de usuários.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailedDTO>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar os dados do Banco de Dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="userId">O ID do usuário a ser recuperado.</param>
        /// <returns>O usuário correspondente ao ID.</returns>
        /// <response code="200">Retorna o usuário encontrado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserDetailedDTO>> GetUserById(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user == null)
                {
                    return NotFound($"Usuário com o ID: {userId}, não foi encontrado!");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar os dados do Banco de Dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="request">Os dados do usuário a ser registrado.</param>
        /// <returns>O UID do usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserRegisterDTO request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!");
                }

                var createdUser = await _authService.RegisterAsync(request);
                return CreatedAtAction(nameof(GetUserById), new
                {
                    userId = request.id_user
                }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar dados no Banco de Dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="oldEmail">O e-mail antigo do usuário a ser atualizado.</param>
        /// <param name="request">Os novos dados do usuário.</param>
        /// <returns>O usuário atualizado.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar os dados no banco de dados.</response>
        [HttpPut("{oldEmail}")]
        public async Task<ActionResult<string>> UpdateUser(string oldEmail, [FromBody] UserUpdateDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!");
                }

                await _authService.UpdateUserByEmailAsync(oldEmail, request);
                return Ok("Usuário Atualizado com Sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar os dados no Banco de Dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="userId">O ID do usuário a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Usuário desativado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro ao deletar o usuário do banco de dados.</response>
        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<string>> DeleteUser(int userId)
        {
            try
            {
                await _authService.DeleteAsync(userId);
                return Ok("Usuário foi desativado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao desativar o usuário do Banco de Dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Reativa um usuário pelo ID.
        /// </summary>
        /// <param name="userId">O ID do usuário a ser reativado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Usuário reativado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro ao reativar o usuário do banco de dados.</response>
        [HttpPost("{userId:int}")]
        public async Task<ActionResult<string>> ReactiveUser(int userId)
        {
            try
            {
                await _authService.ReactiveUserAsync(userId);
                return Ok("Usuário foi reativado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao reativar o usuário do Banco de Dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Redefine a senha pelo E-mail
        /// </summary>
        /// <param name="actualEmail">O E-mail do usuário a ser enviado o link de redefinição.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Link Enviado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro ao enviar e-mail de redefinição de senha.</response>
        [HttpPost("{actualEmail}")]
        public async Task<ActionResult<string>> ForgotPassword(string actualEmail)
        {
            try
            {
                var result = await _authService.ForgotPasswordUserAsync(actualEmail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}