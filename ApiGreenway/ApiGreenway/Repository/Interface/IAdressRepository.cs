using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAddresses();
    Task<Address> GetAddressById(int AddressId);
    Task<Address> AddAddress(Address Address);
    Task<Address> UpdateAddress(Address Address);
    void DeleteAddress(int AddressId);
}
