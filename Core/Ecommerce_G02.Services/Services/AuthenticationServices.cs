using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Shared.DTOs.IdentityDtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Ecommerce_G02.Presistence.Identity.Models;
using Ecommerce_G02.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Ecommerce_G02.Services.Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> usermanager,IConfiguration _configuration,IMapper _mapper) : IAuthenticationServices
    {
       

        #region Login Logic Method
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // check for email
            var user = await usermanager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            var IsPasswordValid = await usermanager.CheckPasswordAsync(user, loginDto.Password);

            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    Email = loginDto.Email,
                    DisplayName = user.DisplayName,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }
        }

        #endregion
        #region Register Logic Method
        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                //from register to application user
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };
            var result = await usermanager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                var error = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(error);
            }
        }

       

        #endregion
        #region Create Token Logic Method
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            /// Instal Package Microsoft.AspNetCore.Authentication.JwtBearer ==>>To Create Token
            var cliams = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var roles = await usermanager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                cliams.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecretKey = _configuration.GetSection("JwtOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken
            (
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                claims: cliams,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: cred
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        #endregion

        // Check Email
        //Take Email And return bool
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await usermanager.FindByEmailAsync(email);
            return user is not null;

        }

        //Get Current user address
        //Take Email And Return Current user Address dto
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user=await usermanager.Users.Include(u=>u.address).FirstOrDefaultAsync(p=>p.Email== email)??throw new UserNotFoundException(email);

            if (user.address is not null)

                return _mapper.Map<Address, AddressDto>(user.address);
            else
                throw new NotUserAddressFound(user.UserName);
            

        }

        //update current user address
        //take email and address updates dto and return   address dto after updates

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await usermanager.Users.Include(u => u.address).FirstOrDefaultAsync(p => p.Email == email) ?? throw new UserNotFoundException(email);

            if (user.address is not null)
            {
                user.address.FirstName = addressDto.FirstName;
                user.address.LastName = addressDto.LastName;
                user.address.City = addressDto.City;
                user.address.Country = addressDto.Country;
                user.address.Country = addressDto.Country;
                user.address.Street = addressDto.Street;

            }
            else
            { 
                var NewAddress = _mapper.Map<AddressDto, Address>(addressDto); 
            }
            await usermanager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.address);
        }

        //Get Current user
        //take email and return user dto
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user=await usermanager.FindByEmailAsync(email)??throw new UserNotFoundException(email);
           
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = email,
                    Token = await CreateTokenAsync(user),
                };
            

        }

    }
}
