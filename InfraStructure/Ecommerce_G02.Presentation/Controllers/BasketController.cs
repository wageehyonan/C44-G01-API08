using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Shared.DTOs.BasketsDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presentation.Controllers
{
    //[ApiController]
    //[Route("api/[Controller]")]
    public class BasketController(IManagerServices _mangerServices):ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string key)
        {
            var basket = await _mangerServices.BasketServices.GetBasketAsync(key);
            return Ok(basket);

        }
        [HttpPost]
        
        public async Task<ActionResult<CustomerBasketDto>> CreateUpdate(CustomerBasketDto basket)
        {
            var internalbasket =await  _mangerServices.BasketServices.CreateUpdateAsync(basket);
            return Ok(internalbasket);

        }
        [HttpDelete("{key}")]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasket(string key) 

        {
            var result=  await _mangerServices.BasketServices.DeleteBasketAsync(key);
            return Ok(result);
        }
    }
}
