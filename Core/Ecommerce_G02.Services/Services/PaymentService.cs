using AutoMapper;
using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Contacts.IUOW;
using Ecommerce_G02.Domain.Exceptions;
using Ecommerce_G02.Domain.Models.Baskets;
using Ecommerce_G02.Domain.Models.Orders;
using Ecommerce_G02.Shared.DTOs.BasketsDtos;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Services
{
    public class PaymentService (IConfiguration configuration,IBasketReposatory BasketRepo,IUnitOfWork unitwork,IMapper maping): IpaymentService
    {
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentAsync(string basketid)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            var basket=await BasketRepo.GetBasketAsync(basketid) ?? throw new BasketNotFoundExceptions(basketid);
            var productRepo = unitwork.GetReposatory<Domain.Models.Products.Product, int>();

           foreach(var item in basket.Items)
            {
                var product=await productRepo.GetByIdAsync(item.Id)?? throw new ProductNotFound(item.Id);
                item.Price= product.Price;
            }
           var deliverymethod=await unitwork.GetReposatory<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value)
                
                                            ?? throw new DeliveryMethodNotFound(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliverymethod.Price;

            var basketamount = (long)(basket.Items.Sum(p => p.Price * p.Quentity)+deliverymethod.Price)*100;

            //          Service 

            var paymentservice = new PaymentIntentService();
            if (basket.PaymentIntentId is null)
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = basketamount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentintent = await paymentservice.CreateAsync(option);
                basket.PaymentIntentId = paymentintent.Id;
                basket.ClientSecret = paymentintent.ClientSecret;


            }

            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = basketamount,
                };
                await paymentservice.UpdateAsync(basket.PaymentIntentId, options);
            }
              await BasketRepo.CreateUpdateAsync(basket);
                return maping.Map<CustomerBasket,CustomerBasketDto>(basket);

            
        }
    }
}
