using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    public class DataBaseFixture : IDisposable
    {
        public MyShopContext Context { get; private set; }

        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<MyShopContext>()
                .UseSqlServer("Server=SRV2\\PUPILS;Database=Test_MyShop_214919813;TrustServerCertificate=True;Trusted_Connection=True", options =>
                {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                })
                .Options;
            Context = new MyShopContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}