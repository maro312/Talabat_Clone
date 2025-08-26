using AutoMapper;
using Domain.Models.Products;
using Shared.Dto_s;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Entity → DTO
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name));

        // CreateDto → Entity
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore());

        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<ProductBrand, BrandDto>();
        CreateMap<ProductType, TypeDto>();
    }
}