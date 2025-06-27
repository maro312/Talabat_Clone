using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Products;
using Shared.Dto_s;

namespace Services;

public class ProductServices(IUnitOfWork unitOfWork,IMapper mapper): IProductServices
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var repo = unitOfWork.GetRepository<Product, int>();
        var products = await repo.GetAllAsync();
        var MappedProducts = mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(products);
        return MappedProducts;
    }

    public async Task<ProductDto> GetProductsByIdAsync(int id)
    {
        var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
        return mapper.Map<Product,ProductDto>(product);
        
    }

    public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
    {
        var repo = unitOfWork.GetRepository<ProductBrand, int>();
        var brands = await repo.GetAllAsync();
        var MappedBrands = mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(brands);
        return MappedBrands;
    }

    public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
    {
        var repo = unitOfWork.GetRepository<ProductType, int>();
        var types = await repo.GetAllAsync();
        var MappedTypes = mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(types);
        return MappedTypes;
    }
}