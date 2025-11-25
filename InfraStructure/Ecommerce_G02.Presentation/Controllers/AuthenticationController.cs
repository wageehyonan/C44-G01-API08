using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Shared.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController(IManagerServices _managerservices):ControllerBase
    {
        [HttpPost("Login")]
        public async Task <ActionResult<UserDto>> Login (LoginDto loginDto)

        {
            var user=await _managerservices.AuthenticationService.LoginAsync(loginDto);
            
              return Ok(user); 
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto registerDto)

        {
            var user = await _managerservices.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }
        /////////////////////////

        [HttpGet("CheckEmail")]
        public async Task <ActionResult<bool>> CheckEmail(string email)
        {

        var checkemail=_managerservices.AuthenticationService.CheckEmailAsync(email);
            return Ok(checkemail);

        }

        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var usermail= User.FindFirstValue(ClaimTypes.Email);
            var currentuser = await _managerservices.AuthenticationService.GetCurrentUserAsync(usermail);
            return Ok(currentuser);
        }

        [HttpGet("UserAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var usermail = User.FindFirstValue(ClaimTypes.Email);

            var  Address=await _managerservices.AuthenticationService.GetCurrentUserAddressAsync(usermail);
            return Ok(Address);
        }

        [HttpPost("UpdateAddress")]

        public async Task<ActionResult<AddressDto>> UpdateCurrentAddress(AddressDto address)
        {
            var usermail = User.FindFirstValue(ClaimTypes.Email);

             
          var updateAddress=await _managerservices.AuthenticationService.UpdateCurrentUserAddressAsync(usermail, address);
            return Ok(updateAddress);

        }
    }

}
