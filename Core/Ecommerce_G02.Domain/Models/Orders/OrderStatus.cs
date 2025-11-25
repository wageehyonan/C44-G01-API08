using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Models.Orders
{
    public enum OrderStatus
    {
        pending=0,
        PaymentRecieved=1,
        PaymentFailed=2
    }
}
