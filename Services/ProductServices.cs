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
    
    public async Task<ProductDto> AddProductAsync(ProductCreateDto productDto)
    {
        var repo = unitOfWork.GetRepository<Product, int>();
        var productEntity = mapper.Map<Product>(productDto);

        repo.Add(productEntity);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<ProductDto>(productEntity);
    }

    public async Task<ProductDto> UpdateProductAsync(int id, ProductCreateDto productDto)
    {
        var repo = unitOfWork.GetRepository<Product, int>();
        var productEntity = await repo.GetByIdAsync(id);

        if (productEntity == null)
            throw new KeyNotFoundException($"Product with id {id} not found.");

        // Map updated fields into existing entity
        mapper.Map(productDto, productEntity);

        repo.Update(productEntity);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<ProductDto>(productEntity);
    }
    public async Task<bool> DeleteProductAsync(int id)
    {
        var repo = unitOfWork.GetRepository<Product, int>();
        var product = await repo.GetByIdAsync(id);

        if (product == null)
            return false; // Not found

        repo.Delete(product);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

}