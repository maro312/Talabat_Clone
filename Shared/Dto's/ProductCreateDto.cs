namespace Shared.Dto_s;

public class ProductCreateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }

    // Foreign keys for relationships
    public int BrandId { get; set; }
    public int TypeId { get; set; }
}