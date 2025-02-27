using Entities;

namespace Services
{
    public interface IRatingService
    {
        Task AddRatingAsync(Rating rating);
    }
}