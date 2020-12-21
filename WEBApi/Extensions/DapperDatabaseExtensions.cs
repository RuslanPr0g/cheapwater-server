using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.DB.DapperSQL;

namespace WEBApi
{
	public static class DapperDatabaseExtensions
	{
		public static IServiceCollection AddDapperDatabase(this IServiceCollection services)
        {
			services.AddScoped<ISQLDataAccess, SQLDataAccess>();
			services.AddScoped<IUserRepository, UserRepository>();
			return services;
        }
	}
}
