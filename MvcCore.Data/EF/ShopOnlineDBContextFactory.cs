using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MvcCore.Data.EF
{
    public class ShopOnlineDBContextFactory : IDesignTimeDbContextFactory<ShopOnlineDBContext>
    {
        public ShopOnlineDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) //Microsoft.Extensions.Configuration.FileExtentions
                .AddJsonFile("appsettings.json") //Microsoft.Extensions.Configuration.Json
                .Build();
            var connectionString = configuration.GetConnectionString("ShopOnlineDatabase");

            // link - https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
            var optionsBuilder = new DbContextOptionsBuilder<ShopOnlineDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ShopOnlineDBContext(optionsBuilder.Options);
        }
    }
}
