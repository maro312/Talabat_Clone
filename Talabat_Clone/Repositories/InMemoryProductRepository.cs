using System.Collections.Concurrent;
using Talabat_Clone.Models;

namespace Talabat_Clone.Repositories;

public class InMemoryProductRepository : IProductRepository
{
	private readonly ConcurrentDictionary<int, Product> _products = new();
	private int _nextId = 1;

	public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		IEnumerable<Product> result = _products.Values.OrderBy(p => p.Id);
		return Task.FromResult(result);
	}

	public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		_products.TryGetValue(id, out var product);
		return Task.FromResult(product);
	}

	public Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
	{
		var id = Interlocked.Increment(ref _nextId);
		product.Id = id;
		_products[id] = product;
		return Task.FromResult(product);
	}

	public Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
	{
		if (!_products.ContainsKey(product.Id))
		{
			return Task.FromResult(false);
		}
		_products[product.Id] = product;
		return Task.FromResult(true);
	}

	public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(_products.TryRemove(id, out _));
	}
}