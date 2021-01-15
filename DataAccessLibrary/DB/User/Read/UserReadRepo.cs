using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public class UserReadRepo : IUserReadRepo
    {
        private readonly ISQLDataAccess _db;

        public UserReadRepo(ISQLDataAccess db)
        {
            this._db = db;
        }

        public async Task<bool> CheckIsEmailPresent(string email, CancellationToken cancellation)
        {
            string sql = "Select Count(*) FROM " + DelimitedString("Users") + " WHERE " 
                + DelimitedString("Email")+" = @Email";
            var p = new
            {
                Email = email
            };
            int emailCount = (await _db.LoadData<int, dynamic>(sql, p, cancellation)).FirstOrDefault();
            if(emailCount==0)
            {
                return false;
            }
            return true;
        }

        private string DelimitedString(string str)
        {
            string delimeter = '"'.ToString();
            return delimeter + str + delimeter;
        }

        public async Task<User> FindUserByEmailAsync(string email, CancellationToken cancellation)
        {
            string sql = @"SELECT * FROM"+DelimitedString("Users")+" WHERE " 
                + DelimitedString("Email")+" = @Email";
            var p = new
            {
                Email = email
            };
            List<User> users = (await _db.LoadData<User, dynamic>(sql, p, cancellation));
            if (users.Count > 1)
            {
                throw new Exception("Duplicate email");
            }
            var user = users.FirstOrDefault();
            return user;
        }

        public async Task<User> FindUserByIdAsync(string id, CancellationToken cancellation)
        {
            string sql = @"SELECT * FROM" + DelimitedString("Users") + " WHERE "
                + DelimitedString("Id") + " = @Id";
            var p = new 
            {
                Id = id
            };
            List<User> users = (await _db.LoadData<User, dynamic>(sql, p, cancellation));
            if(users.Count>1)
            {
                throw new Exception("Duplicate primary key");
            }
            var user = users.FirstOrDefault();
            return user;
        }

    }
}
