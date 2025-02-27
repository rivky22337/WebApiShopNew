using Entities;
using Entities.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        MyShopContext _shopContext;
        IRatingRepository _rating;
        public RatingService(MyShopContext shopContext, IRatingRepository rating)
        {
            _shopContext = shopContext;
            _rating = rating;
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _rating.AddRatingAsync(rating);
        }
    }
}
