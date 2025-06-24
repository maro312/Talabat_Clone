using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);
        builder.HasOne(p=>p.Type)
            .WithMany(b=>b.Products)
            .HasForeignKey(p=>p.TypeId);
        builder.Property(p => p.Price)
            .HasColumnType("decimal(10,3)");
    }
}