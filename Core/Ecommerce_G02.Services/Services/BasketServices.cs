using AutoMapper;
using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Exceptions;
using Ecommerce_G02.Domain.Models.Baskets;
using Ecommerce_G02.Shared.DTOs.BasketsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Services
{
    public class BasketServices (IBasketReposatory _Repo,IMapper _mapper): IBasketServices
    {
        public async  Task<CustomerBasketDto> CreateUpdateAsync(CustomerBasketDto basket, TimeSpan? TimeToLive = null)
        {
            //var customerbasket= _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var customerbasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var basketSaved=  await _Repo.CreateUpdateAsync(customerbasket);
            if (basketSaved != null)
            {
               // return await GetBasketAsync(basketSaved.Id);
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("SomeThing Error in Process");
            }
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _Repo.DeleteBasketAsync(key);
        }

        public async Task<CustomerBasketDto?> GetBasketAsync(string key)
        {
            var basket=await _Repo.GetBasketAsync(key);
            if(basket is not null)
            {
                return   _mapper.Map<CustomerBasket,CustomerBasketDto>(basket);
            }
            else 
            {
                  throw new BasketNotFoundExceptions(key);
                    }
        }
    }
}
