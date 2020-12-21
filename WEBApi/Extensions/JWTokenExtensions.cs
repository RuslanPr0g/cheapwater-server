using DataAccessLibrary.DB.DapperSQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBApi.Authentication;

namespace WEBApi.Extensions
{
    public static class JWTokenExtensions
    {
        public static IServiceCollection AddJWTokens(this IServiceCollection services)
        {
            string key = "This is the key, a very secret one";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
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

            JWTokenManager manager = new JWTokenManager(key, new MockUserRepo());
            services.AddSingleton<IJWTokenManager>((x) => manager);
            return services;
        }
    }
}
