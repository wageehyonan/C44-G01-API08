using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Models.Orders
{
    public class Orders:BaseEntity<Guid>
    {
        public Orders() 
        {
        
        }
        public Orders(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
        public OrderAddress Address { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public  DeliveryMethod DeliveryMethod { get; set; }=null!;

        [ForeignKey("DeliveryMethod")]
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];

        public decimal SubTotal { get; set; }
        public decimal GetTotal()=>SubTotal+DeliveryMethod.Price;

        public string PaymentIntentId { get; set; }
    }
}
