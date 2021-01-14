﻿using DataAccessLibrary.Encryption;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WEBApi.Authentication;
using WEBApi.Validators;
using Microsoft.AspNetCore.Builder;
using WEBApi.Middleware;
using FluentValidation;
using MediatR;
using WEBApi.PipelineBehaviors;

namespace WEBApi.Extensions
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddEncryption(this IServiceCollection services)
        {
            services.AddSingleton<HashAlgorithm>(MD5.Create());
            services.AddSingleton<IEncrypter, Encrypter>();
            return services;
        }

        public static IServiceCollection AddJWTokens(this IServiceCollection services, IConfiguration conf)
        {
            string key = conf.GetValue<string>("Key");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IJWTokenManager, JWTokenManager>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBahavior<,>));
            return services;
        }
    }
}
