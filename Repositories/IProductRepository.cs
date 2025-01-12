using Entities.Models;

namespace Repositories
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProductsAsync(int? minPrice, int? maxPrice, int?[] categoryIds, string? desc);
    }
}