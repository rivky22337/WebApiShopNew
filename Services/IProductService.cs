using Entities.Models;

namespace Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int? minPrice, int? maxPrice, int?[] categoryIds, string? desc);
    }
}