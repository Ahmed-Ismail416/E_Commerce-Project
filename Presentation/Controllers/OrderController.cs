using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrderController(IServiceManager _serviceManager) : BaseController
    {
        //Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDto orderDto)
        {
            var Order = await _serviceManager.OrderService.CreateOrder(orderDto, GetEmailFromToken());
            return Order;

        }
        // Get All Orders By Email
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrdersByEmail()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }
        //Get All Orders
        [Authorize]
        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders(Guid Id)
        {
            var Orders = await _serviceManager.OrderService.GetOrderByIdAsync(Id);
            return Ok(Orders);
        }
    }
}
