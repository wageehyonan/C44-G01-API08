using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Exceptions
{
    public  class BadRequestException(List<string> errors):Exception("Validition failed")
    {
        public List<string> Errors { get; set; } = errors;
    }
}
