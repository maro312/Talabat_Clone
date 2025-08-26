using Microsoft.AspNetCore.Mvc;
using Talabat_Clone.DTOs;
using Talabat_Clone.Models;
using Talabat_Clone.Repositories;

namespace Talabat_Clone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly IProductRepository _repository;

	public ProductsController(IProductRepository repository)
	{
		_repository = repository;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Product>>> GetAll(CancellationToken cancellationToken)
	{
		var products = await _repository.GetAllAsync(cancellationToken);
		return Ok(products);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellationToken)
	{
		var product = await _repository.GetByIdAsync(id, cancellationToken);
		if (product is null) return NotFound();
		return Ok(product);
	}

	[HttpPost]
	public async Task<ActionResult<Product>> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
	{
		var product = new Product
		{
			Name = dto.Name,
			Description = dto.Description,
			Price = dto.Price
		};
		var created = await _repository.CreateAsync(product, cancellationToken);
		return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult> Update(int id, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
	{
		var existing = await _repository.GetByIdAsync(id, cancellationToken);
		if (existing is null) return NotFound();

		existing.Name = dto.Name;
		existing.Description = dto.Description;
		existing.Price = dto.Price;
		var success = await _repository.UpdateAsync(existing, cancellationToken);
		if (!success) return NotFound();
		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
	{
		var success = await _repository.DeleteAsync(id, cancellationToken);
		if (!success) return NotFound();
		return NoContent();
	}
}