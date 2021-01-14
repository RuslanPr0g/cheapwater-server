using DataAccessLibrary.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.Authentication;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLibrary.Encryption;

namespace WEBApi
{
    public class JwtokenManagerFactory : IJwtokenManagerFactory
    {
        private readonly IServiceProvider _provider;

        public JwtokenManagerFactory(IServiceProvider provider)
        {
            this._provider = provider;
        }
        public IJWTokenManager CreateTokenManager()
        {
            /*string key = _provider.GetService<IConfiguration>().GetValue<string>("Key");
            return new JWTokenManager(key, _provider.GetService<IUserReadRepo>(),_provider.GetService<IEncrypter>());*/
            throw new NotImplementedException();
        }
    }
}
