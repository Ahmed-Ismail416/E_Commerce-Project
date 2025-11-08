using DomainLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServiceAbstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<>> GetAllProductsAsync();
        public Task<ProductDto?> GetProductByIdAsync(int id);
        public Task<IEnumerable<ProductBrand>> GetAllProductBrandsAsync();
        public Task<IEnumerable<ProductType>> GetAllProductTypesAsync();


    }
}
