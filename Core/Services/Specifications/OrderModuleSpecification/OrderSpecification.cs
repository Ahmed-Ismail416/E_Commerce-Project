using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.OrderModuleSpecification
{
    internal class OrderSpecification: BaseSpecification<Order, Guid>
    {
        //Ge By Email
        public OrderSpecification(string Email) : base(x => x.UserEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(OrderBy => OrderBy.OrderDate);
        }
        //Get By Id 
        public OrderSpecification(Guid Id) : base(x => x.Id == Id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(OrderBy => OrderBy.OrderDate);
        }

    }
}
