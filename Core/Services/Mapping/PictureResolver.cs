using AutoMapper;
using DomainLayer.Models.Products;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class PictureResolver(IConfiguration _config) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (source.PictureUrl == null)
                return string.Empty;
            return $"{_config.GetSection("Urls")["baseUrl"]}/{source.PictureUrl}";
        }
    }
}
