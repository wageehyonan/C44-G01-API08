using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Models.Orders
{
    public class OrderItem:BaseEntity<int>
    {
        public decimal Price {  get; set; }
        public int Quentity {  get; set; }
        public ProductItemOrdered Product {  get; set; }
    }
}
