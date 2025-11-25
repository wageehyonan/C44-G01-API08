using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Models.Products
{
    public class ProductType:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
