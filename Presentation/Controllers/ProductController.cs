using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DTOs.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
   
    public class ProductController(IServiceManager _serviceManager) : BaseController
    {
        //Get All Products
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginateResult<ProductDto>>> GetAllProducts([FromQuery] ProductParam QueryParam)
        {
            var Products =  await _serviceManager.ProductService.GetAllProductsAsync(QueryParam);
            return Ok(Products);
        }

        //Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            if (Product == null)
                return NotFound();
            return Ok(Product);
        }
        //Get All Product Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrandDto>>> GetAllProductBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllProductBrandsAsync();
            return Ok(Brands);
        }
        //Get All Product Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetAllProductTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllProductTypesAsync();
            return Ok(Types);
        }


    }
}
