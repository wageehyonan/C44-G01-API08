using Ecommerce_G02.Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Specifications
{
    public class OrderPaymentIntenIdSpecification : BaseSpecification<Orders, Guid>
    {
        public OrderPaymentIntenIdSpecification(string intentid) : base(o=>o.PaymentIntentId==intentid)
        {
        }
    }
}
