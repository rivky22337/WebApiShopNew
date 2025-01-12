using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        MyShopContext _context;
        public CategoryRepository(MyShopContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync<Category>();
        }

    }
}
