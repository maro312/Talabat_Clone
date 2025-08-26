using Talabat_Clone.Models;

namespace Talabat_Clone.Repositories;

public interface IProductRepository
{
	Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
	Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}