using AutoMapper;
using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Domain.Contacts.IUOW;
using Ecommerce_G02.Domain.Exceptions;
using Ecommerce_G02.Domain.Models.Products;
using Ecommerce_G02.Services.Specifications;
using Ecommerce_G02.Shared.Common;
using Ecommerce_G02.Shared.DTOs.ProductsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Services
{
    public class ProductServices(IUnitOfWork _UnitOfWork,IMapper _Mapper) : IproductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo=_UnitOfWork.GetReposatory<ProductBrand,int>();
            var Brands = await  repo.GetAllAsync();
           var BrandsDto= _Mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

      //  public async Task<IEnumerable<ProductDto>> GetAllProductAsync(ProductQueryParms? _ProductQueryParm)
        public async Task<PaginationResult<ProductDto>> GetAllProductAsync(ProductQueryParms? _ProductQueryParm)
        {
            var spec=new ProductSpecifications(_ProductQueryParm);
            var products = await _UnitOfWork.GetReposatory<Product, int>().GetAllWithSpecificationAsync(spec);
            // var products = await _UnitOfWork.GetReposatory<Product, int>().GetAllAsync();
            // replaced by pagination//var ProductsDto = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var Data = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            int size=Data.Count();

            var countSpecification = new CountProductSpecification(_ProductQueryParm);
            var count = await _UnitOfWork.GetReposatory<Product,int>().GetCountWithSpecificationAsync(countSpecification);
            return  new PaginationResult<ProductDto>(_ProductQueryParm.PageIndex,size, count, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _UnitOfWork.GetReposatory<ProductType, int>().GetAllAsync();
            var typedto = _Mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            return typedto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecifications(id);

            var product = await _UnitOfWork.GetReposatory<Product, int>().GetByIdWithSpecifiactionAsync(spec);
           if (product is null)
            {
                throw new ProductNotFound(id);
            }
            var productdto = _Mapper.Map<Product,ProductDto>(product);
            return productdto;

        }
    }
}
