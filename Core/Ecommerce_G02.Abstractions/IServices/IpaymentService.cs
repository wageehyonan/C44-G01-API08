using Ecommerce_G02.Shared.DTOs.BasketsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Abstractions.IServices
{
    public interface IpaymentService
    {
        public Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketid);
    }
}
