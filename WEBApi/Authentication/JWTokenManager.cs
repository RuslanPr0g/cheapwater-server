﻿using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using DataAccessLibrary.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WEBApi.Authentication
{
    public class JWTokenManager : IJWTokenManager
    {
        private readonly string key;
        private readonly IUserReadRepo _repo;
        private readonly IEncrypter _encrypter;

        public JWTokenManager(IConfiguration config, IUserReadRepo repo, IEncrypter encrypter)
        {
            this.key = config.GetValue<string>("Key");
            this._repo = repo;
            this._encrypter = encrypter;
        }
        public async Task<string> Authorize(string email, string password, CancellationToken cancellation)
        {
            User user = await _repo.FindUserByEmailAsync(email, cancellation);

            string encryptedPassword = await _encrypter.Encrypt(password);

            if((user is not null)&&(user.Password.Equals(encryptedPassword)))
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenKey = Encoding.ASCII.GetBytes(key);
                var TokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = TokenHandler.CreateToken(TokenDescriptor);
                return TokenHandler.WriteToken(token);
            }
            else
            {
                return null;
            }
        }
    }
}
