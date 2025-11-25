using AutoMapper;
using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Contacts.IUOW;
using Ecommerce_G02.Domain.Exceptions;
using Ecommerce_G02.Domain.Models.Orders;
using Ecommerce_G02.Domain.Models.Products;
using Ecommerce_G02.Presistence.Identity.Models;
using Ecommerce_G02.Services.Specifications;
using Ecommerce_G02.Shared.DTOs.IdentityDtos;
using Ecommerce_G02.Shared.DTOs.OderDtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Services
{
    public class OrderServices(IMapper mapping,IBasketReposatory BasktRepo,IUnitOfWork _UnitWork) : IOrderServices
    {
        #region $$$$$$$$$$$$$$$$$$$$$$$$$$ Create Order  $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
		
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto _OrderDto, string Email)
        {       //Mapping Address
            var Address = mapping.Map<AddressDto, OrderAddress>(_OrderDto.Address);
            /// Get Basket
            var basket = await BasktRepo.GetBasketAsync(_OrderDto.BasketId) ?? throw new BasketNotFoundExceptions(_OrderDto.BasketId);

            //##################  For Check Paymentintent basket

            ArgumentNullException.ThrowIfNullOrEmpty(basket.PaymentIntentId);

            var orderRepo = _UnitWork.GetReposatory<Orders, Guid>();

            var spec = new OrderPaymentIntenIdSpecification(basket.PaymentIntentId);
            var existdOrder = await orderRepo.GetByIdWithSpecifiactionAsync(spec);
            if (existdOrder != null)
            {
                orderRepo.Delete(existdOrder);
            }
            //####################################################33

            // List To Fill From Basket  and check items(product) in Table Product0*****
            List<OrderItem> OrderItemList = [];

            var productRepo = _UnitWork.GetReposatory<Product, int>();

            foreach (var item in basket.Items)
            {
                var ProductR = await productRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                var _orderitems = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = ProductR.Id,
                        ProductName = ProductR.Name,
                        PictureUrl = ProductR.PictureUrl,
                    },
                    Quentity = item.Quentity,
                    Price = ProductR.Price,


                };
                OrderItemList.Add(_orderitems);
            }

            //Get Delivery Method
            var Deliverymethod = await _UnitWork.GetReposatory<DeliveryMethod, int>().GetByIdAsync(_OrderDto.DeliveryMethodId) ?? throw new DeliveryMethodNotFound(_OrderDto.DeliveryMethodId);
            //Get Sub Total

            var subtotal = OrderItemList.Sum(p => p.Price * p.Quentity);
            var order = new Orders(Email, Address, Deliverymethod, OrderItemList, subtotal, basket.PaymentIntentId);

            _UnitWork.GetReposatory<Orders, Guid>().Add(order);

            _UnitWork.SaveChangesAsync();
            return mapping.Map<Orders, OrderToReturnDto>(order);

        }

        #endregion
        
        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync()
        {
           var delivermethos=await _UnitWork.GetReposatory<DeliveryMethod,int>().GetAllAsync();
            return mapping.Map<IEnumerable<DeliveryMethod>,IEnumerable<DeliveryMethodDto>>(delivermethos);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var orderSpecifiactions=new OrderSpecifications(Email);
            var allorders = await _UnitWork.GetReposatory<Orders, Guid>().GetAllWithSpecificationAsync(orderSpecifiactions);
            return mapping.Map<IEnumerable<Orders>,IEnumerable<OrderToReturnDto>>(allorders);
        }

        public async Task<OrderToReturnDto> GetOrderByIDAsync(Guid orderid)
        {
            var orderSpec = new OrderSpecifications(orderid);
            var order=await _UnitWork.GetReposatory<Orders,Guid>().GetByIdWithSpecifiactionAsync(orderSpec);
            return mapping.Map<Orders,OrderToReturnDto>(order);
        }
    }
}
