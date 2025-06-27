using Shared.Dto_s;

namespace Abstraction;

public interface IProductServices
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();   
    Task<ProductDto> GetProductsByIdAsync(int id);
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();   
    Task<IEnumerable<TypeDto>> GetAllTypesAsync();   
}