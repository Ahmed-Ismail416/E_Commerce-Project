using DomainLayer.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Specifications
{
    internal class ProductWithBrandTypeSpecification : BaseSpecification<Product, int>
    {
        // Get All Products with Brand and Type, Filteration
        public ProductWithBrandTypeSpecification(ProductParam QueryParams) : base
            (
                p => (!QueryParams.BrandId.HasValue|| p.ProductBrandId == QueryParams.BrandId) &&
                (!QueryParams.TypeId.HasValue || p.ProductTypeId == QueryParams.TypeId) &&
                (String.IsNullOrEmpty(QueryParams.Search) || p.Name.ToLower().Contains(QueryParams.Search.ToLower()))

            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch(QueryParams.SortingOptions)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
            ApplyPaging(QueryParams.PageSize, QueryParams.PageIndex);

        }

        // Get Product by Id with Brand and Type
        public ProductWithBrandTypeSpecification(int id): base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
