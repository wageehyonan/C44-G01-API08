using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Models
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } 
    
    }
}
