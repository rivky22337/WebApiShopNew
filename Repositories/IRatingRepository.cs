using Entities;

namespace Repositories
{
    internal interface IRatingRepository
    {
        Task AddRatingAsync(Rating rating);
    }
}