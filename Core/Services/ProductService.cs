using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Products;
using ServiceAbstraction;
using Services.Specifications;
using Shared;
using Shared.DTOs.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductBrandDto>> GetAllProductBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<ProductBrandDto>>(Brands);
       
        }

        public async Task<PaginateResult<ProductDto>> GetAllProductsAsync(ProductParam QueryParams)
        {
            var spec = new ProductWithBrandTypeSpecification(QueryParams);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            var AllProductsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = AllProductsDto.Count();
            var specCount = new ProudctCountSpecification(QueryParams);
            var CountAllProduct = await _unitOfWork.GetRepository<Product,int>().CountAsync(specCount);
            return new PaginateResult<ProductDto>( QueryParams.PageIndex, ProductCount ,CountAllProduct, AllProductsDto);
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeDto>>(Types);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandTypeSpecification(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            if(Product is null)
                throw new ProductNotFoundException(id);
            return _mapper.Map<Product, ProductDto>(Product);
        }
    }
}
