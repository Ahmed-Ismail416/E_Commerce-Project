using AutoMapper;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.Products;
using Shared.DTOs.IdentityDtos;
using Shared.DTOs.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
           
            CreateMap<AddressDto, Address>().ReverseMap();
            
        }
    }
}
