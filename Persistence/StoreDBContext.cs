using System.Reflection;
using System.Reflection.Metadata;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class StoreDBContext(DbContextOptions<StoreDBContext> options) : DbContext(options)
{
    // public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
    // {
    //     
    // }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }
    public DbSet<ProductType> Types { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    =>modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
}