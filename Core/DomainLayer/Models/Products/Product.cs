using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Products
{
    public class Product :BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        //navigation
        public ProductBrand ProductBrand { get; set; } = null!;
        public int ProductBrandId { get; set; }

        public ProductType ProductType { get; set; } = null!;
        public int ProductTypeId { get; set; }
    }
}
