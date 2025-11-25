using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Shared.DTOs.OderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")]
    public class OrdersController(IManagerServices _mangerService):ControllerBase
    {
       
        [HttpPost]
        public async Task <ActionResult<OrderToReturnDto>> CreateOrder(OrderDto _OrderDto)
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var order = await _mangerService.OrderServices.CreateOrderAsync(_OrderDto, email);

            return Ok(order);
        }

        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            var result=await _mangerService.OrderServices.GetAllDeliveryMethodsAsync();
            return Ok(result);

        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {  
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var result=await _mangerService.OrderServices.GetAllOrdersAsync(Email);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(Guid ID)
        {
            
            var result = await _mangerService.OrderServices.GetOrderByIDAsync(ID);
            return Ok(result);
        }

    }
}
