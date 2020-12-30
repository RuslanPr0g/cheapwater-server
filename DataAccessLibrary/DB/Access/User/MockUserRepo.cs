using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public class MockUserRepo : IUserReadRepo
    {
        public Task<bool> CheckIsEmailPresent(string email, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindUserByEmailAsync(string email, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindUserByIdAsync(string email, CancellationToken token)
        {
            return new User { Email = "Vasyka2@gmail.com", Password = "123" };
        }

    }
}
