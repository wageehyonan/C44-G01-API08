using Ecommerce_G02.Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Specifications
{
    public class OrderSpecifications:BaseSpecification<Orders,Guid>
    {
        /// Specification By Email
        public OrderSpecifications(string email):base(o=>o.UserEmail==email) 
        
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o=>o.Items);

            AddOrderByDesc(o => o.OrderDate);
        }
        /// Specification By Order ID
        public OrderSpecifications(Guid ID) : base(o => o.Id == ID)

        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);

            
        }
    }
}
