using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Encryption
{
    public class Encrypter : IEncrypter
    {
        private readonly HashAlgorithm _algorithm;

        public Encrypter(HashAlgorithm algorithm)
        {
            this._algorithm = algorithm;
        }
        public async Task<string> Encrypt(string ToBeEncrypt)
        {
            var bytes = Encoding.UTF8.GetBytes(ToBeEncrypt);
            var hash = _algorithm.ComputeHash(bytes);
            string output = Encoding.UTF8.GetString(hash);
            return output;
        }
    }
}
