using DTO;
using Entities.Models;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(Order order);
        Task<GetOrderDTO> GetOrderByIdAsync(int id);
    }
}