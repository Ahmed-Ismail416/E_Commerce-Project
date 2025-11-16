using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductParam
    {
        const int  DefualtPageSize = 5;
        const int MaxPageSize = 10;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions? SortingOptions { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        int pageseize = DefualtPageSize;
        public int PageSize
        {
            get => pageseize;
            set => pageseize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
