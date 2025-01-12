using DTO;
using Entities.Models;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<GetOrderDTO> GetOrderByIdAsync(int id);
    }
}