using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence.Identity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public Address? address { get; set; }

    }
}
