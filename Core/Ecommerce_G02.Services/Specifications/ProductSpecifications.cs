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
    public class ProductSpecifications : BaseSpecification<Product, int>
    {
        public ProductSpecifications(ProductQueryParms? _ProductQueryParm) : base(p=>(!_ProductQueryParm.BrandId.HasValue||p.BrandId==_ProductQueryParm.BrandId)&&(!_ProductQueryParm.TypeId.HasValue||p.TypeId== _ProductQueryParm.TypeId)
                                            &&(string.IsNullOrEmpty(_ProductQueryParm.SearchValue))|| p.Name.ToLower().Contains(_ProductQueryParm.SearchValue.ToLower()))
        {
            AddInclude(p=>p.Brand);
            AddInclude(p => p.Type);

            switch(_ProductQueryParm.sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                    default:
                    break;
            }

            ApplayMigination( _ProductQueryParm.PageSize, _ProductQueryParm.PageIndex);
        }

       
        public ProductSpecifications(int id ) : base(p=>p.Id==id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);


        }

    }
}
