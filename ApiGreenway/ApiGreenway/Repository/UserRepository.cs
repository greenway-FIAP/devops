using ApiGreenway.Data;
using ApiGreenway.Models;
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

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _dbContext.Users.Where(u => u.dt_finished_at == null).ToListAsync();
    }

    public async Task<User> GetUserById(int UserId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.id_user == UserId && u.dt_finished_at == null);
    }

    public async Task<User> AddUser(User user)
    {
        // Verifica se o Email já está cadastrado
        if (await _dbContext.Users.AnyAsync(u => u.ds_email == user.ds_email && u.dt_finished_at == null))
        {
            throw new Exception("E-mail já cadastrado!");
        }

        // Hash da Senha
        user.ds_password = BCrypt.Net.BCrypt.HashPassword(user.ds_password);

        // Adiciona o User no Banco de Dados
        var userDb = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return userDb.Entity;
    }

    // Login
    public async Task<User> Login(string ds_email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.ds_email == ds_email && u.dt_finished_at == null);
    }

    public async Task<User> UpdateUser(User user)
    {
        var userDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.id_user == user.id_user);
        if (userDb == null)
        {
            return null; // Retorna null se o User não for encontrado
        }

        userDb.ds_email = user.ds_email;

        // Atualiza a senha, garantindo que ela seja criptografada... Caso esteja vazia não atualiza
        if (!string.IsNullOrEmpty(user.ds_password))
        {
            userDb.ds_password = BCrypt.Net.BCrypt.HashPassword(user.ds_password);
        }

        userDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        userDb.id_user_type = user.id_user_type;
        userDb.id_company_representative = user.id_company_representative;

        await _dbContext.SaveChangesAsync();
        return userDb;
    }

    public async void DeleteUser(int UserId)
    {
        var userDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.id_user == UserId);
        if (userDb != null)
        {
            userDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
