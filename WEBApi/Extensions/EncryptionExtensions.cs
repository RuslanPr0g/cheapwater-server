using DataAccessLibrary.Encryption;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WEBApi.Extensions
{
    public static class EncryptionExtensions
    {
        public static IServiceCollection AddEncryption(this IServiceCollection services)
        {
            services.AddSingleton<HashAlgorithm>(MD5.Create());
            services.AddSingleton<IEncrypter, Encrypter>();
            return services;
        }
    }
}
