using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public interface IUserRepository
    {
        Task<User> FindUserByIdAsync(string id);
        Task InsertUserIntoTheDb(User user);
    }
}
