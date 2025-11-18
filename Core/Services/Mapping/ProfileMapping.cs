using AutoMapper;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.DTOs.BasketModuleDtos;
using Shared.DTOs.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl,
                           opt => opt.MapFrom<PictureResolver>());
            CreateMap<ProductBrand, ProductBrandDto>();
            CreateMap<ProductType, ProductTypeDto>();


        }
    }
}
