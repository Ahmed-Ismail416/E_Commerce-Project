using AutoMapper;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.IdentityDtos;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(dest => dest.DeliveryMethod,
                           opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.OrderStatus,
                           opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Total,
                           opt => opt.MapFrom(src => src.GetTotal()));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl,
                           opt => opt.MapFrom<OrderPictureResolver>());

        }

     
    }
}
