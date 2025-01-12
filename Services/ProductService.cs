using Entities.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(int? minPrice, int? maxPrice, int?[] categoryIds, string? desc)
        {
            return await _productRepository.GetProductsAsync(minPrice, maxPrice, categoryIds, desc);
        }
    }
}
