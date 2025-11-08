using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var Products =  await _serviceManager.ProductService.GetAllProductsAsync();
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
