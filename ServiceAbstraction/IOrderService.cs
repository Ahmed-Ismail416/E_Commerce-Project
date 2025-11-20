using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrder(OrderDto orderDto, string Email);
        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email);
        Task<OrderToReturnDTO> GetOrderByIdAsync(Guid id);
    }
}
