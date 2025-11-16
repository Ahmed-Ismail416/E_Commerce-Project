using Shared;
using Shared.DTOs.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServiceAbstraction
{
    public interface IProductService
    {
        public Task<PaginateResult<ProductDto>> GetAllProductsAsync(ProductParam QueryParams);
        public Task<ProductDto?> GetProductByIdAsync(int id);
        public Task<IEnumerable<ProductBrandDto>> GetAllProductBrandsAsync();
        public Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync();


    }
}
