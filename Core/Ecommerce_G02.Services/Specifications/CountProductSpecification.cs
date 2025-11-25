using Ecommerce_G02.Domain.Models.Products;
using Ecommerce_G02.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Specifications
{
    public class CountProductSpecification:BaseSpecification<Product,int>
    {
        public CountProductSpecification(ProductQueryParms? _ProductQueryParm) : base(p => (!_ProductQueryParm.BrandId.HasValue || p.BrandId == _ProductQueryParm.BrandId) && (!_ProductQueryParm.TypeId.HasValue || p.TypeId == _ProductQueryParm.TypeId)
                                            && (string.IsNullOrEmpty(_ProductQueryParm.SearchValue)) || p.Name.ToLower().Contains(_ProductQueryParm.SearchValue.ToLower()))
        {

        }

        
    }
}
