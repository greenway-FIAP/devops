using ApiGreenway.Data;
using ApiGreenway.Models;
using ApiGreenway.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiGreenway.Repository;

public class ProductRepository : IProductRepository
{
    private readonly dbContext _dbContext;

    public ProductRepository(dbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _dbContext.Products.Where(p => p.dt_finished_at == null).ToListAsync();
    }

    public async Task<Product> GetProductById(int ProductId)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.id_product == ProductId && p.dt_finished_at == null);
    }
    
    public async Task<Product> AddProduct(Product product)
    {
        var productDb = await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return productDb.Entity;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var productDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.id_product == product.id_product);
        if (productDb == null)
        {
            return null; // Retorna null se o Product não for encontrado
        }

        productDb.ds_name = product.ds_name;
        productDb.tx_description = product.tx_description;
        productDb.vl_sale_price = product.vl_sale_price;
        productDb.vl_cost_price = product.vl_cost_price;
        productDb.nr_weight = product.nr_weight;
        productDb.dt_updated_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
        productDb.id_product_type = product.id_product_type;

        await _dbContext.SaveChangesAsync();
        return productDb;

    }

    public async void DeleteProduct(int ProductId)
    {
        var productDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.id_product == ProductId);
        if (productDb != null)
        {
            productDb.dt_finished_at = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)); // UTC-3 Brasília
            await _dbContext.SaveChangesAsync();
        }
    }
}
