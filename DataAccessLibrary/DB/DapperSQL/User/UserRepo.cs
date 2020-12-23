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

        public async Task<UserModel> FindUserByIdAsync(string id)
        {
            string sql = @"SELECT * FROM USERS WHERE Id = @Id";
            var p = new 
            {
                Id = id
            };
            List<UserModel> users = (await _db.LoadData<UserModel, dynamic>(sql, p));
            if(users.Count>1)
            {
                throw new Exception("Duplicate primary key");
            }
            var user = users.FirstOrDefault();
            return user;
        }

        public async Task InsertUserIntoTheDb(UserModel user)
        {
            string sql = @"INSERT Users(Id, Email, Password, Nickname) 
                Values(UserID, Email, Password, Nickname)";
            await _db.SaveData<UserModel>(sql, user);
        }
    }
}
