using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WEBApi.Authentication
{
    public class JWTokenManager : IJWTokenManager
    {
        private readonly string key;
        private readonly IUserReadRepository _repo;

        public JWTokenManager(string key, IUserReadRepository repo)
        {
            this.key = key;
            this._repo = repo;
        }
        public async Task<string> Authorize(string email, string password)
        {
            User user = await _repo.FindUserByEmailAsync(email);

            if((user is not null)&&(user.Password.Equals(password)))
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenKey = Encoding.ASCII.GetBytes(key);
                var TokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
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
