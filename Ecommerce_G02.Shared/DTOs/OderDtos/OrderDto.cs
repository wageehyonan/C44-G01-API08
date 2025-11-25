using Ecommerce_G02.Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Shared.DTOs.OderDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId   { get; set; }
        public AddressDto Address { get; set; }
    }
}
