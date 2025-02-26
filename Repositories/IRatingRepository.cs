using Entities;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task AddRatingAsync(Rating rating);
    }
}