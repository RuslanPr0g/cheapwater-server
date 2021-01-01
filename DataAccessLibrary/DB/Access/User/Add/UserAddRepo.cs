
using DataAccessLibrary.DB.Entities;
using DataAccessLibrary.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public class UserAddRepo:IUserAddRepo
    {
        private readonly AuthContext _context;
        private readonly IEncrypter _encrypter;

        public UserAddRepo(AuthContext context, IEncrypter encrypter)
        {
            this._context = context;
            this._encrypter = encrypter;
        }

        public async Task InsertUserIntoTheDb(User user)
        {
            string encryptedPassword = await _encrypter.Encrypt(user.Password);
            user.Password = encryptedPassword;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
