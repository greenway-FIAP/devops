using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Models.Dtos;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class UserRepository : IUserRepository
{
    private readonly dbContext _dbContext;

    public UserRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    // Métodos CRUD

    public async Task<IEnumerable<UserDetailedDTO>> GetUsers()
    {
        var users = await _dbContext.Users.Where(u => u.ds_uid_fb != null & u.dt_finished_at == null).ToListAsync() ?? throw new Exception("Não há usuários cadastrados!");

        var usersDTO = users.Select(u => new UserDetailedDTO
        {
            id_user = u.id_user,
            ds_email = u.ds_email,
        });
        return usersDTO;
    }

    public async Task<UserDetailedDTO> GetUserById(int UserId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.id_user == UserId && u.dt_finished_at == null) ?? throw new Exception("Usuário não encontrado ou excluído!");

        var userDTO = new UserDetailedDTO
        {
            id_user = user.id_user,
            ds_email = user.ds_email
        };
        return userDTO;
    }
}
