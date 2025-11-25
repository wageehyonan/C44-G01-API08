using AutoMapper;
using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Contacts.IUOW;
using Ecommerce_G02.Presistence.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Services
{
    public class ManagerServices(IMapper map, IUnitOfWork uow,IBasketReposatory _RepoBasket,UserManager<ApplicationUser> usermanger,IConfiguration _configuration) : IManagerServices
    {
        private readonly Lazy<IproductServices> LazyProduct_services = new Lazy<IproductServices>(() => new ProductServices(uow, map));
        public IproductServices ProductServices => LazyProduct_services.Value;

        // For Basket
        private readonly Lazy<IBasketServices> LazyBasket_Services = new Lazy<IBasketServices>(() => new BasketServices(_RepoBasket, map));
        public IBasketServices BasketServices => LazyBasket_Services.Value;

        private readonly Lazy<IAuthenticationServices> Lazy_AuthenticatioServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(usermanger, _configuration, map));
        public IAuthenticationServices AuthenticationService => Lazy_AuthenticatioServices.Value;


        private readonly Lazy<IOrderServices> Lazy_OrdersServices = new Lazy<IOrderServices>(() => new  OrderServices(map, _RepoBasket, uow));
        public IOrderServices OrderServices => Lazy_OrdersServices.Value;


        private readonly Lazy<IpaymentService> Lazy_PaymentServices = new Lazy<IpaymentService>(() => new PaymentService(_configuration, _RepoBasket, uow, map));

        public IpaymentService PaymentService => Lazy_PaymentServices.Value;
    }
}
