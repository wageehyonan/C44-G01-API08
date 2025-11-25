using Ecommerce_G02.Shared.Common;
using Ecommerce_G02.Shared.DTOs.ProductsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Abstractions.IServices
{
    public interface IproductServices
    {
        Task<PaginationResult<ProductDto>> GetAllProductAsync(ProductQueryParms? _ProductQueryParm);

        Task<ProductDto> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
