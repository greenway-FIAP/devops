using ApiGreenway.Models;

namespace ApiGreenway.Repository.Interface;

public interface IProductTypeRepository
{
    Task<IEnumerable<ProductType>> GetProductTypes();
    Task<ProductType> GetProductTypeById(int ProductTypeId);
    Task<ProductType> AddProductType(ProductType productType);
    Task<ProductType> UpdateProductType(ProductType productType);
    void DeleteProductType(int ProductTypeId);
}
