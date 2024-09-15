using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IUserTypeRepository
{
    Task<IEnumerable<UserType>> GetUserTypes();
    Task<UserType> GetUserTypeById(int UserTypeId);
    Task<UserType> AddUserType(UserType userType);
    Task<UserType> UpdateUserType(UserType userType);
    void DeleteUserType(int UserTypeId);
}
