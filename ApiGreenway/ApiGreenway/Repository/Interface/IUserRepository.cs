using ApiGreenway.Models;
using ApiGreenway.Models.Dtos;

namespace ApiGreenway.Repository.Interface;

public interface IUserRepository
{
    Task<IEnumerable<UserDetailedDTO>> GetUsers();
    Task<UserDetailedDTO> GetUserById(int UserId);
}
