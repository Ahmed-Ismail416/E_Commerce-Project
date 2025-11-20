using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.Products;
using ServiceAbstraction;
using Services.Specifications.OrderModuleSpecification;
using Shared.DTOs.IdentityDtos;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IBasketRepository _basketrepository,
                                IUnitOfWork _unitOfWork,
                                IMapper _mapper) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrder(OrderDto orderDto, string Email)
        {
            // Basket items
            var BasketItems = await _basketrepository.GetBasketAsync(orderDto.BaskedId)
                ?? throw new BasketNotFoundException(orderDto.BaskedId);

            List<OrderItem> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product,int>();
            foreach (var item in BasketItems.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                
                orderItems.Add(CreateOrderItem(item, product));
            }
            // Address
            var OrderAddress = _mapper.Map<AddressDto,OrderAddress>(orderDto.Address);
            // Delivery method
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(orderDto.DelvierMethod) ??
                throw new DeliveryMethodNotFoundException(orderDto.DelvierMethod);
            // subtotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            //create model
            var order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, subtotal);
            //create order
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return  _mapper.Map<Order, OrderToReturnDTO>(order);
        }


        // GetAllOrdersAsync
        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email)
        {
            var spec = new OrderSpecification(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(Orders);
        }

        //GetOrderByIdAsync
        public async Task<OrderToReturnDTO> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
            return _mapper.Map<Order, OrderToReturnDTO>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Product = new ProductOrderItem()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl
                },
                Price = product.Price,
                Quantity = item.Quantity
            };
        }
    }
}
