using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.DB;

namespace WEBApi.Extensions
{
	public static class DapperDatabaseExtensions
	{
		public static IServiceCollection AddDapperDatabase(this IServiceCollection services)
        {
			services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
			services.AddSingleton<IUserReadRepository, UserReadRepo>();
			return services;
        }
	}
}
