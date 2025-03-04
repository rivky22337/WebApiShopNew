
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;

namespace TestProject
{
    public class DataBaseFixture : IDisposable
    {
        public MyShopContext Context { get; private set; }
        //IConfiguration configuration;
        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<MyShopContext>()
                .UseSqlServer("Server=SRV2\\PUPILS;Database=Test_MyShop_214919813;TrustServerCertificate=True;Trusted_Connection=True")
                .Options;
            Context = new MyShopContext(options);
            //Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
