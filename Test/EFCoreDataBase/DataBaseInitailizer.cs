using Lib.DataManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Test;

public static class DataBaseInitailizer
{
    public static MonitorDbContext GetContext()
    {
        Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
        
        var configuration = new ConfigurationBuilder()
                           .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var context = new MonitorDbContext(new DbContextOptionsBuilder<MonitorDbContext>()
                                          .UseSqlServer(connectionString)
                                          .Options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}