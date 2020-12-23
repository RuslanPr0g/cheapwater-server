using DataAccessLibrary.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public interface IUserRepository
    {
        Task<UserModel> FindUserByIdAsync(string id);
        Task InsertUserIntoTheDb(UserModel user);
    }
}
