using Ecommerce_G02.Shared.DTOs.OderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Abstractions.IServices
{
    public interface IOrderServices
    {
        public Task<OrderToReturnDto> CreateOrderAsync(OrderDto _OrderDto, string Email);
        // get al delivery methods
        public Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync();

        // get all orders for user
        public Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);
        //Get Spasefic Order For user
        public Task<OrderToReturnDto> GetOrderByIDAsync(Guid orderid);
    }
}
