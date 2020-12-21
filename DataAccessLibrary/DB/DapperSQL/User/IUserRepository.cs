using DataAccessLibrary.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB.DapperSQL
{
    public interface IUserRepository
    {
        Task<UserModel> FindUserByEmailAsync(string email);
    }
}
