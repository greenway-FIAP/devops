using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class UserTypeRepository : IUserTypeRepository
{
    private readonly dbContext _dbContext;

    public UserTypeRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<UserType>> GetUserTypes()
    {
        return await _dbContext.UserTypes.Where(u => u.dt_finished_at == null).ToListAsync();
    }

    public async Task<UserType> GetUserTypeById(int UserTypeId)
    {
        return await _dbContext.UserTypes.FirstOrDefaultAsync(u => u.id_user_type == UserTypeId && u.dt_finished_at == null);
    }

    public async Task<UserType> AddUserType(UserType userType)
    {
        var userTypeDb = await _dbContext.UserTypes.AddAsync(userType);
        await _dbContext.SaveChangesAsync();
        return userTypeDb.Entity;
    }

    public async Task<UserType> UpdateUserType(UserType userType)
    {
        var userTypeDb = await _dbContext.UserTypes.FirstOrDefaultAsync(u => u.id_user_type == userType.id_user_type);
        if (userTypeDb == null)
        {
            return null; // Retorna null se o UserType não for encontrado
        }

        userTypeDb.ds_title = userType.ds_title;
        userTypeDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

        await _dbContext.SaveChangesAsync();
        return userTypeDb;
    }

    public async void DeleteUserType(int UserTypeId)
    {
        var userTypeDb = await _dbContext.UserTypes.FirstOrDefaultAsync(u => u.id_user_type == UserTypeId);
        if (userTypeDb != null)
        {
            userTypeDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
