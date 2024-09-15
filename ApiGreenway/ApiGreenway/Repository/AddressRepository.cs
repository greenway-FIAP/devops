using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly dbContext _dbContext;

    public AddressRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Address>> GetAddresses()
    {
        return await _dbContext.Addresses.Where(a => a.dt_finished_at == null).ToListAsync();
    }

    public async Task<Address> GetAddressById(int addressId)
    {
        return await _dbContext.Addresses.FirstOrDefaultAsync(d => d.id_address == addressId && d.dt_finished_at == null);
    }

    public async Task<Address> AddAddress(Address address)
    {
        var addressBd = await _dbContext.Addresses.AddAsync(address);
        await _dbContext.SaveChangesAsync();
        return addressBd.Entity;
    }

    public async Task<Address> UpdateAddress(Address address)
    {
        var addressDb = await _dbContext.Addresses.FirstOrDefaultAsync(d => d.id_address == address.id_address);
        if (addressDb == null)
        {
            return null; // Retorna null se o Address não for encontrado
        }

        // Atualiza o nome da rua
        addressDb.ds_street = address.ds_street;
        addressDb.ds_zip_code = address.ds_zip_code;
        addressDb.ds_number = address.ds_number;
        addressDb.ds_uf = address.ds_uf;
        addressDb.ds_neighborhood = address.ds_neighborhood;
        addressDb.ds_city = address.ds_city;
        addressDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        addressDb.id_company = address.id_company;

        await _dbContext.SaveChangesAsync();
        return addressDb;
    }

    public async void DeleteAddress(int addressId)
    {
        var addressDb = await _dbContext.Addresses.FirstOrDefaultAsync(d => d.id_address == addressId);
        if (addressDb != null)
        {
            addressDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

            // Atualiza o status do Address para finalizado
            await _dbContext.SaveChangesAsync();
        }
    }
}
