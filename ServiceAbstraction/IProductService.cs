using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServiceAbstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        public Task<ProductDto?> GetProductByIdAsync(int id);
        public Task<IEnumerable<ProductBrandDto>> GetAllProductBrandsAsync();
        public Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync();


    }
}
