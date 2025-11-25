using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Shared.DTOs.BasketsDtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; } = null!;
        
        public ICollection<BasketItemDto> Items { get; set; }= null!;

        //    For Payment
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
