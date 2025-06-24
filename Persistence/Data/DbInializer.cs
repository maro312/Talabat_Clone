using System.Text.Json;
using Domain.Contracts;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class DbInializer(StoreDBContext context) : IDbInializer
{
    public async Task InializeAsync()
    {
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            context.Database.MigrateAsync();
        }

        try
        {
            if (!context.Set<ProductBrand>().Any())
            {
                var Data =
                    await File.ReadAllTextAsync(
                        @"..\Persistence\Data\Seeds\brands.json");
                var objects = JsonSerializer.Deserialize<List<ProductBrand>>(Data);
                if (objects is not null && objects.Any())
                {
                    context.Set<ProductBrand>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
        
            if (!context.Set<ProductType>().Any())
            {
                var Data =
                    await File.ReadAllTextAsync(
                        @"..\Persistence\Data\Seeds\types.json");
                var objects = JsonSerializer.Deserialize<List<ProductType>>(Data);
                if (objects is not null && objects.Any())
                {
                    context.Set<ProductType>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
        
            if (!context.Set<Product>().Any())
            {
                var Data =
                    await File.ReadAllTextAsync(
                        @"..\Persistence\Data\Seeds\products.json");
                var objects = JsonSerializer.Deserialize<List<Product>>(Data);
                if (objects is not null && objects.Any())
                {
                    context.Set<Product>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        
    }
}