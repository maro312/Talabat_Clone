namespace Domain.Models.Products;

public class ProductBrand : ModelBase<int>
{
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}