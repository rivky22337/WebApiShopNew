using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        MyShopContext _context;
        public ProductRepository(MyShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int? minPrice, int? maxPrice, int?[] categoryIds, string? desc)
        {
            var query = _context.Products.Where(Product =>
          (desc == null ? (true) : (Product.ProductName.Contains(desc)))
          && (minPrice == null ? (true) : (Product.Price >= minPrice))
          && ((maxPrice == null) ? (true) : (Product.Price <= maxPrice))
          && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(Product.CategoryId))))
          .OrderBy(Product => Product.Price).Include(p => p.Category);
           
            List<Product> products = await query.ToListAsync();
            return products;

        }
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync<Product>(p => p.ProductId == id);
        }

    }
}
