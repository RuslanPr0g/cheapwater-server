using DataAccessLibrary.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB.DapperSQL
{
    public class UserRepository : IUserRepository
    {
        private readonly ISQLDataAccess _db;

        public UserRepository(ISQLDataAccess db)
        {
            this._db = db;
        }

        public Task<UserModel> FindUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
