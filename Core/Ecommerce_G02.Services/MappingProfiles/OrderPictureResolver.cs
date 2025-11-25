using AutoMapper;
using Ecommerce_G02.Domain.Models.Orders;
using Ecommerce_G02.Shared.DTOs.OderDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.MappingProfiles
{
    public class OrderPictureResolver(IConfiguration _configuartion) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{_configuartion.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }
           
        }
    }
}
