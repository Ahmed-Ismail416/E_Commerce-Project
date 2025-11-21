//using AutoMapper;
//using DomainLayer.Contracts;
//using DomainLayer.Models.IdentityModule;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.Extensions.Configuration;
//using ServiceAbstraction;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services
//{
//    public class ServiceManager(IUnitOfWork unitOfWork,
//                                IMapper mapper,
//                                IBasketRepository _basketrepository,
//                                UserManager<ApplicationUser> _userManager,
//                                IConfiguration _config,
//                                IMapper _mapper)
//    {
//        public Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

//        public Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketrepository, mapper));

//        public Lazy<IAuthunticationService> _athunticationService = new Lazy<IAuthunticationService>(() => new AuthenticationService(_userManager,_config,_mapper));
        
//        public Lazy<IOrderService> _orderService = new Lazy<IOrderService>(() => new OrderService(_basketrepository,unitOfWork,mapper));
        
//        public IOrderService OrderService => _orderService.Value;

//        public IProductService ProductService => _productService.Value;

//        public IBasketService BasketService => _basketService.Value;

//        public IAuthunticationService authunticationService => _athunticationService.Value;
//    }
//}
