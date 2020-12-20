using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DataAccessLibrary;

namespace WEBApi
{
	public static class DapperDatabaseExtensions
	{
		public static IServiceCollection AddDapperDatabase(this IServiceCollection services)
        {
			return services;
        }
	}
}
