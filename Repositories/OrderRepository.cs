using AutoMapper;
using DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        MyShopContext _context;
        IMapper _mapper;
        public OrderRepository(MyShopContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetOrderDTO> GetOrderByIdAsync(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync<Order>(p => p.OrderId == id);
            if (order == null)  return null;
            GetOrderDTO orderDTO = _mapper.Map<Order, GetOrderDTO>(order);
            var query = _context.OrderItems.Where(orderItem=>orderItem.OrderId == id);
            List<OrderItem> orderItems = await query.ToListAsync();
            foreach (var item in orderItems)
            {
                OrderItemDTO itemDTO = _mapper.Map<OrderItem, OrderItemDTO>(item);
                orderDTO.OrderItems.Add(itemDTO);
            }
            return orderDTO;

        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            if (order == null)
                return null;
            else
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }



    }
}
