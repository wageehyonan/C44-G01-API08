
using AutoMapper;
using Azure;
using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Contacts.IUOW;
using Ecommerce_G02.Domain.Contacts.Seed;
using Ecommerce_G02.Presistence.Contexts;
using Ecommerce_G02.Presistence.Identity.Models;
using Ecommerce_G02.Presistence.Repos;
using Ecommerce_G02.Presistence.Seed;
using Ecommerce_G02.Presistence.UOW;
using Ecommerce_G02.Services.MappingProfiles;
using Ecommerce_G02.Services.Services;
using Ecommerce_G02.Shared.ErrorModels;
using Ecommerce_G02.Web.CustomMiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using StackExchange.Redis;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            
            #region DataBase s Contexts
            // For Main DataBase Context
            builder.Services.AddDbContext<StoreDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            }

            );
            // For Identity Users Roles Context
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            }

            );

            // For Basket And Rdies

            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            }
                );

            //builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

            #endregion
            //********************************************

            builder.Services.AddAutoMapper(p => p.AddProfile(new ProjectProfile()));

            builder.Services.AddScoped<IDataSeed, DataSeed>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IManagerServices, ManagerServices>();
            builder.Services.AddScoped<IBasketReposatory, BasketReposatory>();

            builder.Services.AddScoped<ICacheReposatory, CacheReposatory>();
            builder.Services.AddScoped<ICacheServices, CacheServices>();

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores< StoreIdentityDbContext >();
            // Add Service For Authentication And Tokens
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }

            ).AddJwtBearer(options=>
                
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtOptions:Audience"],
                        ValidateLifetime= true,
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
                    };

            }
                
                );

            //***********************************
          
                                                                                        
            
            // For Validation Errors
            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var error = context.ModelState.Where(M => M.Value.Errors.Any())
                                                .Select(M => new Shared.ErrorModels.ValidationError()
                                                {
                                                    Field = M.Key,
                                                    Errors=M.Value.Errors.Select(e=>e.ErrorMessage),

                                                } );
                    var response = new ValidationErrorsToReturn()
                    {
                        ValidationErrors= error
                    };
                    return new BadRequestObjectResult(response);
                };
               
            });
            // Add services to the Controllers.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
           
            /// To Active Service Manul without dependancy injection to fill data seed
            var scoped=app.Services.CreateScope();
           var ObjectDataSeeding=scoped.ServiceProvider.GetRequiredService<IDataSeed>();
           await  ObjectDataSeeding.DataSeedingAsync();
            await ObjectDataSeeding.IdentityDataSeedingAsync();
            ////////////////////////// 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            //for Handel Errors
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
