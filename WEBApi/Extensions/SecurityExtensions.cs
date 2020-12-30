using DataAccessLibrary.Encryption;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;


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
    }
}
