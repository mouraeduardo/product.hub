using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> 
{

    public AppDbContext CreateDbContext(string[] args) {
        //var configuration = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json")
        //    .Build();

        //var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Server=localhost;port=5432;user id=postgres;Password=1234;database=ProductHub;pooling=true");

        return new AppDbContext(optionsBuilder.Options);
    }

}
