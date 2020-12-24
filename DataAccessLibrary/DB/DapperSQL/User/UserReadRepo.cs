using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public class UserReadRepo : IUserReadRepository
    {
        private readonly ISQLDataAccess _db;

        public UserReadRepo(ISQLDataAccess db)
        {
            this._db = db;
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            string sql = @"SELECT * FROM Users WHERE Id = @Id";
            var p = new 
            {
                Id = id
            };
            List<User> users = (await _db.LoadData<User, dynamic>(sql, p));
            if(users.Count>1)
            {
                throw new Exception("Duplicate primary key");
            }
            var user = users.FirstOrDefault();
            return user;
        }

    }
}
