using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Contacts.Seed
{
    public interface IDataSeed
    {
         Task DataSeedingAsync();

        Task IdentityDataSeedingAsync();
    }
}
