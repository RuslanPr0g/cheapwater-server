using Microsoft.Extensions.DependencyInjection;
using DataAccessLibrary;
using DataAccessLibrary.DB;
using Microsoft.AspNetCore.Builder;
using WEBApi.Middleware;
using Dapper;
using DataAccessLibrary.DB.Access.DataAccess;
using System;

namespace WEBApi.Extensions
{
    public static class DataAccessExtentions
    {
        public static IServiceCollection AddDapperDatabase(this IServiceCollection services)
        {
            services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
            services.AddSingleton<IUserReadRepo, UserReadRepo>();

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
