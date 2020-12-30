using Microsoft.Extensions.DependencyInjection;
using DataAccessLibrary;
using DataAccessLibrary.DB;

namespace WEBApi.Extensions
{
    public static class DataAccessExtentions
    {
        public static IServiceCollection AddDapperDatabase(this IServiceCollection services)
        {
            services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
            services.AddSingleton<IUserReadRepo, UserReadRepo>();
            return services;
        }
    }
}
