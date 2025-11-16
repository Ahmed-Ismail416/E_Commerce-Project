using DomainLayer.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProudctCountSpecification : BaseSpecification<Product, int>
    {
        public ProudctCountSpecification(ProductParam QueryParams): base
            (
                p => (!QueryParams.BrandId.HasValue || p.ProductBrandId == QueryParams.BrandId) &&
                (!QueryParams.TypeId.HasValue || p.ProductTypeId == QueryParams.TypeId) &&
                (String.IsNullOrEmpty(QueryParams.Search) || p.Name.ToLower().Contains(QueryParams.Search.ToLower()))

            )
        {
            
        }
    }
}
