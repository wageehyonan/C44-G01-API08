using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Exceptions
{
    public class UnAuthorizedException(string message="Inavalid Password "):Exception(message)
    {
    }
}
