using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace TestProject
{

    public class ProductRepositoryIntegrationTest
    {

        private readonly DbContextOptions<MyShopContext> _options;
        private readonly IConfiguration _configuration;

        public ProductRepositoryIntegrationTest()
        {
            // Create a configuration for testing
            var inMemorySettings = new Dictionary<string, string> {
                {"DBContext_Connection:School", "Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _options = new DbContextOptionsBuilder<MyShopContext>()
                .UseSqlServer(_configuration.GetSection("DBContext_Connection")["School"])
                .Options;

            // Seed the database with test data
            using (var context = new MyShopContext(_options, _configuration))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Set IDENTITY_INSERT to ON for CATEGORIES
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT CATEGORIES ON");
                context.Categories.AddRange(
                    new Category { CategoryId = 1, CategoryName = "Category1" },
                    new Category { CategoryId = 2, CategoryName = "Category2" }
                );
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT CATEGORIES OFF");

                // Set IDENTITY_INSERT to ON for PRODUCTS
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PRODUCTS ON");
                context.Products.AddRange(
                    new Product { ProductId = 1, ProductName = "Product1", Price = 100, CategoryId = 1 },
                    new Product { ProductId = 2, ProductName = "Product2", Price = 200, CategoryId = 2 },
                    new Product { ProductId = 3, ProductName = "Product3", Price = 300, CategoryId = 1 }
                );
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PRODUCTS OFF");
            }
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnAllProducts_WhenNoFilterIsApplied()
        {
            // Arrange
            using (var context = new MyShopContext(_options, _configuration))
            {
                var repository = new ProductRepository(context);

                // Act
                var products = await repository.GetProductsAsync(null, null, new int?[] { }, null);

                // Assert
                Assert.Equal(3, products.Count());
            }
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnFilteredProducts_ByPriceRange()
        {
            // Arrange
            using (var context = new MyShopContext(_options, _configuration))
            {
                var repository = new ProductRepository(context);

                // Act
                var products = await repository.GetProductsAsync(150, 250, new int?[] { }, null);

                // Assert
                Assert.Single(products);
                Assert.Equal("Product2", products.First().ProductName);
            }
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnFilteredProducts_ByCategory()
        {
            // Arrange
            using (var context = new MyShopContext(_options, _configuration))
            {
                var repository = new ProductRepository(context);

                // Act
                var products = await repository.GetProductsAsync(null, null, new int?[] { 1 }, null);

                // Assert
                Assert.Equal(2, products.Count());
                Assert.All(products, p => Assert.Equal(1, p.CategoryId));
            }
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnFilteredProducts_ByDescription()
        {
            // Arrange
            using (var context = new MyShopContext(_options, _configuration))
            {
                var repository = new ProductRepository(context);

                // Act
                var products = await repository.GetProductsAsync(null, null, new int?[] { }, "Product1");

                // Assert
                Assert.Single(products);
                Assert.Equal("Product1", products.First().ProductName);
            }
        }

        [Fact]
        public async Task GetProductById_ShouldReturnCorrectProduct()
        {
            // Arrange
            using (var context = new MyShopContext(_options, _configuration))
            {
                var repository = new ProductRepository(context);

                // Act
                var product = await repository.GetProductById(1);

                // Assert
                Assert.NotNull(product);
                Assert.Equal(1, product.ProductId);
                Assert.Equal("Product1", product.ProductName);
            }
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            using (var context = new MyShopContext(_options, _configuration))
            {
                var repository = new ProductRepository(context);

                // Act
                var product = await repository.GetProductById(999);

                // Assert
                Assert.Null(product);
            }
        }
    }
    }

