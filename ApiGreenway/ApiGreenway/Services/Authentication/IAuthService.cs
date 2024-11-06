using ApiGreenway.Models;
using ApiGreenway.Models.Dtos;

namespace ApiGreenway.Services.Authentication;

/// <summary>
/// Interface para o serviço de autenticação.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Serviço de Registrar usuário.
    /// </summary>
    Task<string> RegisterAsync(UserRegisterDTO request);

    /// <summary>
    /// Serviço de Logar usuário.
    /// </summary>
    Task<string> LoginAsync(UserLoginDTO request);

    /// <summary>
    /// Serviço de Atualizar usuário.
    /// </summary>
    Task<string> UpdateUserByEmailAsync(string oldEmail, UserUpdateDto request);

    /// <summary>
    /// Serviço de Deletar usuário.
    /// </summary>
    Task<string> DeleteAsync(int userId);

    /// <summary>
    /// Serviço de Reativar usuário.
    /// </summary>
    Task<string> ReactiveUserAsync(int userId);

    /// <summary>
    /// Serviço de Recuperar a Senha
    /// </summary>
    Task<string> ForgotPasswordUserAsync(string actualEmail);
}
