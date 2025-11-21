using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> _productService,
        Func<IBasketService> _basketService,
        Func<IAuthunticationService> _authunticationService,
        Func<IOrderService> _orderService) : IServiceManager
    {
        public IProductService ProductService => _productService.Invoke();

        public IBasketService BasketService => _basketService.Invoke();

        public IAuthunticationService authunticationService => _authunticationService.Invoke();

        public IOrderService OrderService => _orderService.Invoke();
    }
}
