using Abstraction;
using Shared.Dto_s;

namespace Presentation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IServicesManger servicesManger) : ControllerBase
{
    //GetAllProduct
    [HttpGet]
    //baseurl/api/Product
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var Products = await servicesManger.ProductServices.GetAllProductsAsync();
        return Ok(Products);
    }
    //GetAllBrands
    [HttpGet("brands")]
    //baseurl/api/Product/brands
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
    {
        var Brands = await servicesManger.ProductServices.GetAllBrandsAsync();
        return Ok(Brands);
    }
    //GetAllBrands
    [HttpGet("types")]
    //baseurl/api/Product/types
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
    {
        var Types = await servicesManger.ProductServices.GetAllTypesAsync();
        return Ok(Types);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var Product = await servicesManger.ProductServices.GetProductsByIdAsync(id);
        return Ok(Product);
    }
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductCreateDto productDto)
    {
        if (productDto == null)
            return BadRequest("Product data is required");

        var createdProduct = await servicesManger.ProductServices.AddProductAsync(productDto);

        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromBody] ProductCreateDto productDto)
    {
        if (productDto == null)
            return BadRequest("Product data is required");

        try
        {
            var updatedProduct = await servicesManger.ProductServices.UpdateProductAsync(id, productDto);
            return Ok(updatedProduct);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await servicesManger.ProductServices.DeleteProductAsync(id);

        if (!result)
            return NotFound($"Product with id {id} not found.");

        return NoContent(); // 204 No Content
    }

    
}