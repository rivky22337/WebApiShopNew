using Microsoft.Extensions.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DataBaseFixture
    {
        public MyShopContext Context { get;private set; }
        public DataBaseFixture()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var options = new DbContextOptionsBuilder<MyShopContext>()
                .UseSqlServer("Server=DESKTOP-QKU0HL3;Database=ManageShop;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            Context = new MyShopContext(options, configuration);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
