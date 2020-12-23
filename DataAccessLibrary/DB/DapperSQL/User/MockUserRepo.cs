using DataAccessLibrary.DB.Entities;
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
        public async Task<User> FindUserByIdAsync(string email)
        {
            return new User { Email = "Vasyka2@gmail.com", Password = "123" };
        }

        public Task InsertUserIntoTheDb(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
