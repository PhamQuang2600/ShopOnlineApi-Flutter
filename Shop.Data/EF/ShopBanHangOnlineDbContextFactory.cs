using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.EF
{
    public class ShopBanHangOnlineDbContextFactory : IDesignTimeDbContextFactory<ShopOnlineAppContext>
    {
        
            public ShopOnlineAppContext CreateDbContext(string[] arg)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var connectionString = configuration.GetConnectionString("ShopOnlineApp");
                var optionsbuilder = new DbContextOptionsBuilder<ShopOnlineAppContext>();
                optionsbuilder.UseSqlServer(connectionString);
                return new ShopOnlineAppContext(optionsbuilder.Options);
            }
        
    }
}
