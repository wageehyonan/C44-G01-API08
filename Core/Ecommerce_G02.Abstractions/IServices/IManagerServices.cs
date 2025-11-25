using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Abstractions.IServices
{
    public interface IManagerServices
    {
        public IproductServices ProductServices { get; }
        public IBasketServices BasketServices { get; }

        public IAuthenticationServices AuthenticationService { get; }

        public IOrderServices OrderServices { get; }

        public IpaymentService PaymentService { get; }
    }
}
