using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Shared.Common;
using Ecommerce_G02.Shared.DTOs.ProductsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presentation.Controllers
{
    //[ApiController]
    //[Route("api/[Controller]")]
    [Authorize]
    public class ProductController(IManagerServices _ManagerService):ApiBaseController

    {
       
        [HttpGet]
        // public async Task <ActionResult<IEnumerable<ProductDto>>> GetAllProducts(int? BrandId ,int? TypeId,ProductSortingOptions? sortingOption)
       // public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParms? _ProductQueryParm)
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParms? _ProductQueryParm)
        {
            var products = await _ManagerService.ProductServices.GetAllProductAsync(_ProductQueryParm);
            return Ok(products);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _ManagerService.ProductServices.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var Types = await _ManagerService.ProductServices.GetAllTypesAsync();
            return Ok(Types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByID(int id)
        {
            var Product = await _ManagerService.ProductServices.GetProductByIdAsync(id);
            return Ok(Product);
        }
    }
}
