using DataAccessLibrary.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public class MockUserRepo : IUserRepository
    {
        public async Task<UserModel> FindUserByIdAsync(string email)
        {
            return new UserModel { Email = "Vasyka2@gmail.com", Nickname = "Vasyka", Password = "123" };
        }

        public Task InsertUserIntoTheDb(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
