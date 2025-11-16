using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository _basketrepository) : IServiceManager
    {
        public Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _productService.Value;

        public Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketrepository, mapper));
        public IBasketService BasketService => _basketService.Value;

    }
}
