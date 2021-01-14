using Microsoft.Extensions.DependencyInjection;
using DataAccessLibrary;
using DataAccessLibrary.DB;
using Microsoft.AspNetCore.Builder;
using WEBApi.Middleware;
using Dapper;
using DataAccessLibrary.DB.Access.DataAccess;
using System;
using WEBApi.DTOs;

namespace WEBApi.Extensions
{
    public static class DataAccessExtentions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISQLDataAccess, SQLDataAccess>();
            services.AddScoped<IUserReadRepo, UserReadRepo>();
            services.AddScoped<IUserAddRepo, UserAddRepo>();

            services.AddSingleton<IModelConverter, ModelConverter>();

            SqlMapper.AddTypeHandler(new PostgreGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            return services;
        }

        public static IApplicationBuilder UseWebSocketsServer(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.UseMiddleware<WebSocketsMiddleware>();
            return app;
        }
    }
}
