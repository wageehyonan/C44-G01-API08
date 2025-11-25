using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Exceptions
{
    public sealed class ProductNotFound(int id):NotFoundException($"Product {id} Is Not Found ")
    {
    }
}
