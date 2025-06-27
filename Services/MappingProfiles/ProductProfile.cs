using AutoMapper;
using Domain.Models.Products;
using Shared.Dto_s;

namespace Services.MappingProfiles;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dist=>dist.BrandName, opt=>opt.MapFrom(src=>src.Brand.Name))
            .ForMember(dist=>dist.TypeName, opt=>opt.MapFrom(src=>src.Type.Name));
        CreateMap<ProductBrand, BrandDto>();
        CreateMap<ProductType, TypeDto>();
    }
}