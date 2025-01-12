using Entities.Models;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
    }
}