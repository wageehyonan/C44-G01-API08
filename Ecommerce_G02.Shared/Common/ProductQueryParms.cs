using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Shared.Common
{
    public class ProductQueryParms
    {
        private const int DefualtSize= 5;
        private const int Maxsize = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions? sortingOption { get; set; }

        public string? SearchValue { get; set; }
        public int PageIndex { get; set; } = 1;
        
        private int pagesize=DefualtSize;
        public int PageSize { get { return pagesize; } set { pagesize = value > Maxsize ? Maxsize :  value; } }
    }
}
