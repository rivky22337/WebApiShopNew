using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        MyShopContext _shopContext;
        public RatingRepository(MyShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _shopContext.Ratings.AddAsync(rating);
            await _shopContext.SaveChangesAsync();
        }
    }
}
