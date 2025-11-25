using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Shared.DTOs.BasketsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IManagerServices _managerServices):ControllerBase
    {

        [Authorize]
        [HttpPost("{basketid}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var  basket=await _managerServices.PaymentService.CreateOrUpdatePaymentIntentAsync(basketid);
            return Ok(basket);


        }
    }
}
