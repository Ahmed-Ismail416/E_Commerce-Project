using AutoMapper;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class OrderPictureResolver(IConfiguration _config) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (source.Product.PictureUrl == null)
                return string.Empty;
            var Url = $"{_config.GetSection("Urls")["baseUrl"]}/{source.Product.PictureUrl}";
            return Url;
        }
    }
}
