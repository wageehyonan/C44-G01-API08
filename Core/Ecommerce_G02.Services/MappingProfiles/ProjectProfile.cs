using AutoMapper;
using Ecommerce_G02.Domain.Models.Baskets;
using Ecommerce_G02.Domain.Models.Orders;
using Ecommerce_G02.Domain.Models.Products;
using Ecommerce_G02.Presistence.Identity.Models;
using Ecommerce_G02.Shared.DTOs.BasketsDtos;
using Ecommerce_G02.Shared.DTOs.IdentityDtos;
using Ecommerce_G02.Shared.DTOs.OderDtos;
using Ecommerce_G02.Shared.DTOs.ProductsDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ecommerce_G02.Services.MappingProfiles
{
    public class ProjectProfile:Profile
    {
        //public ProjectProfile() { }
        public ProjectProfile(/*IConfiguration _configuartion*/)
        {
            #region Product  =========>>>>>>> Mapping
            CreateMap<Product,ProductDto>()
                        .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.Brand.Name))
                        .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.Type.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>(); 
            #endregion
            #region Basket ======>>>>>>> MApping

            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
           
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
           
            #endregion

            #region Address  ======>>>>>>> Mapping
            CreateMap<Address,AddressDto>().ReverseMap();

            #endregion
            #region Orders  =====>>>> Mapping
            CreateMap<Orders,OrderToReturnDto>().ForMember(d => d.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName));
            CreateMap<AddressDto,OrderAddress>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ForMember(m => m.ProductName, options => options.MapFrom(src => src.Product.ProductName));
                                           //    .ForMember(m => m.PictureUrl, options => options.MapFrom(new OrderPictureResolver(_configuartion)));

            #endregion

            #region Delivey Methods ======>>>>>> Mapping
            CreateMap<DeliveryMethod,DeliveryMethodDto>().ReverseMap();
            #endregion

        }

    }
}
