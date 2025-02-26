using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class RatingService
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
            return await _rating.AddRatingAsync(rating);
        }
    }
}
