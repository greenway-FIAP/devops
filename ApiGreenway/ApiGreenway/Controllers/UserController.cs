using ApiGreenway.Models;
using ApiGreenway.Repository;
using ApiGreenway.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiGreenway.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._configuration = configuration;
        }

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        /// <response code="200">Retorna a lista de usuários.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="userId">O ID do usuário.</param>
        /// <returns>O usuário correspondente ao ID.</returns>
        /// <response code="200">Retorna o usuário encontrado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro ao recuperar os dados do banco de dados.</response>
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar os dados do Banco de Dados");
            }
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="user">O usuário a ser registrado.</param>
        /// <returns>O usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao adicionar dados no banco de dados.</response>
        [HttpPost("register")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                var createdUser = await _userRepository.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new
                {
                    userId = createdUser.id_user
                }, createdUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Faz login de um usuário.
        /// </summary>
        /// <param name="user">O usuário a ser autenticado.</param>
        /// <returns>Um token JWT se o login for bem-sucedido.</returns>
        /// <response code="200">Login bem-sucedido.</response>
        /// <response code="400">E-mail ou senha inválidos.</response>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.ds_email) || string.IsNullOrEmpty(user.ds_password))
            {
                return BadRequest("E-mail ou senha inválidos.");
            }

            var existingUser = await _userRepository.Login(user.ds_email);
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.ds_password, existingUser.ds_password))
            {
                return BadRequest("E-mail ou senha inválidos.");
            }

            string token = CreateToken(existingUser);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.ds_email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="userId">O ID do usuário a ser atualizado.</param>
        /// <param name="user">Os novos dados do usuário.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro ao atualizar os dados no banco de dados.</response>
        [HttpPut("{userId:int}")]
        public async Task<ActionResult<string>> UpdateUser(int userId, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Alguns dados estão inválidos, verifique!!");
                }

                user.id_user = userId;
                var updatedUser = await _userRepository.UpdateUser(user);
                if (updatedUser == null)
                {
                    return NotFound($"Usuário com o ID: {userId}, não foi encontrado!");
                }

                return Ok("Usuário Atualizado com Sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar os dados no Banco de Dados");
            }
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="userId">O ID do usuário a ser deletado.</param>
        /// <returns>Uma mensagem de sucesso.</returns>
        /// <response code="200">Usuário deletado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro ao deletar os dados do banco de dados.</response>
        [HttpDelete("{userId}")]
        public async Task<ActionResult<string>> DeleteUser(int userId)
        {
            try
            {
                var deletedUser = await _userRepository.GetUserById(userId);
                if (deletedUser == null)
                {
                    return NotFound($"Usuário com o ID: {userId}, não foi encontrado!");
                }

                _userRepository.DeleteUser(userId);
                return Ok("Usuário foi Deletado com Sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar os dados do Banco de Dados");
            }
        }
    }
}