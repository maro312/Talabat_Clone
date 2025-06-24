using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Talabat_Clone;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region DI Container Services
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<StoreDBContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IDbInializer, DbInializer>();
        
        #endregion
        var app = builder.Build();

        await InializeDbAsync(app);    
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    public static async Task InializeDbAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbInializer = scope.ServiceProvider.GetRequiredService<IDbInializer>();
        await dbInializer.InializeAsync();
    }
}