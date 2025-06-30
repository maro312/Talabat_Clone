using Abstraction;
using AutoMapper;
using Domain.Contracts;

namespace Services;

public class ServicesManger(IUnitOfWork unitOfWork, IMapper mapper) : IServicesManger
{
    private readonly Lazy<IProductServices> _lazyProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));

    public IProductServices ProductServices => _lazyProductServices.Value;
    
}