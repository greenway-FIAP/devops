using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly dbContext _dbContext;

    public ProductTypeRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<ProductType>> GetProductTypes()
    {
        return await _dbContext.ProductTypes.Where(p => p.dt_finished_at == null).ToListAsync();
    }

    public async Task<ProductType> GetProductTypeById(int ProductTypeId)
    {
        return await _dbContext.ProductTypes.FirstOrDefaultAsync(p => p.id_product_type == ProductTypeId && p.dt_finished_at == null);
    }

    public async Task<ProductType> AddProductType(ProductType productType)
    {
        var productTypeDb = await _dbContext.ProductTypes.AddAsync(productType);
        await _dbContext.SaveChangesAsync();
        return productTypeDb.Entity;
    }

    public async Task<ProductType> UpdateProductType(ProductType productType)
    {
        var productTypeDb = await _dbContext.ProductTypes.FirstOrDefaultAsync(p => p.id_product_type == productType.id_product_type);
        if (productTypeDb == null)
        {
            return null; // Retorna null se o ProductType não for encontrado
        }

        productTypeDb.ds_name = productType.ds_name;
        productTypeDb.tx_description = productType.tx_description;
        productTypeDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília

        await _dbContext.SaveChangesAsync();
        return productTypeDb;
    }

    public async void DeleteProductType(int ProductTypeId)
    {
        var productTypeDb = await _dbContext.ProductTypes.FirstOrDefaultAsync(p => p.id_product_type == ProductTypeId);
        if (productTypeDb != null)
        {
            productTypeDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
