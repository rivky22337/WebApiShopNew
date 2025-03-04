using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Linq;
using System.Threading.Tasks;
using TestProject;
using Xunit;

namespace IntegrationTests
{
    public class ProductRepositoryTests : IClassFixture<DataBaseFixture>
    {
        private readonly MyShopContext _context;

        public ProductRepositoryTests(DataBaseFixture fixture)
        {
            _context = fixture.Context;
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(
                    new Category { CategoryName = "Category1" },
                    new Category { CategoryName = "Category2" }
                );
                _context.SaveChanges();
            }

            if (!_context.Products.Any())
            {
                var categories = _context.Categories.ToList();
                _context.Products.AddRange(
                    new Product { ProductName = "Product1", Price = 100, CategoryId = categories[0].CategoryId },
                    new Product { ProductName = "Product2", Price = 200, CategoryId = categories[1].CategoryId },
                    new Product { ProductName = "Product3", Price = 300, CategoryId = categories[0].CategoryId }
                );
                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnAllProducts_WhenNoFilterIsApplied()
        {
            var repository = new ProductRepository(_context);
            var products = await repository.GetProductsAsync(null, null, new int?[] { }, null);
            Assert.Equal(3, products.Count());
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnFilteredProducts_ByPriceRange()
        {
            var repository = new ProductRepository(_context);
            var products = await repository.GetProductsAsync(150, 250, new int?[] { }, null);
            Assert.Single(products);
            Assert.Equal("Product2", products.First().ProductName);
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnFilteredProducts_ByCategory()
        {
            var repository = new ProductRepository(_context);
            var products = await repository.GetProductsAsync(null, null, new int?[] { 1 }, null);
            Assert.Equal(2, products.Count());
            Assert.All(products, p => Assert.Equal(1, p.CategoryId));
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnFilteredProducts_ByDescription()
        {
            var repository = new ProductRepository(_context);
            var products = await repository.GetProductsAsync(null, null, new int?[] { }, "Product1");
            Assert.Single(products);
            Assert.Equal("Product1", products.First().ProductName);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnCorrectProduct()
        {
            var repository = new ProductRepository(_context);
            var product = await repository.GetProductById(1);
            Assert.NotNull(product);
            Assert.Equal(1, product.ProductId);
            Assert.Equal("Product1", product.ProductName);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            var repository = new ProductRepository(_context);
            var product = await repository.GetProductById(999);
            Assert.Null(product);
        }
    }
}