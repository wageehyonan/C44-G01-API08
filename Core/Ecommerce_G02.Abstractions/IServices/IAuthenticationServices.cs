using Ecommerce_G02.Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Abstractions.IServices
{
    public interface IAuthenticationServices
    {
       public Task <UserDto> LoginAsync(LoginDto loginDto);
       public  Task<UserDto> RegisterAsync(RegisterDto registerDto);

        // Check Email
        //Take Email And return bool
        public Task <bool> CheckEmailAsync(string email);
        //Get Current user address
        //Take Email And Return Current user Address dto
        public Task<AddressDto> GetCurrentUserAddressAsync(string email);

        //update current user address
        //take email and address updates dto and return   address dto after updates

        public Task<AddressDto> UpdateCurrentUserAddressAsync(string email,AddressDto addressDto);

        //Get Current user
        //take email and return user dto

        public Task<UserDto> GetCurrentUserAsync(string email);

    }
}
