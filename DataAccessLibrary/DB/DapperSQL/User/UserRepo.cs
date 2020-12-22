using DataAccessLibrary.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public class UserRepo : IUserRepository
    {
        private readonly ISQLDataAccess _db;

        public UserRepo(ISQLDataAccess db)
        {
            this._db = db;
        }

        public Task<UserModel> FindUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
