using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(int ProductId);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    void DeleteProduct(int ProductId);
}
