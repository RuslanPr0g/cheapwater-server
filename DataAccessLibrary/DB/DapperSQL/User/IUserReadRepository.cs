using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public interface IUserReadRepository
    {
        Task<User> FindUserByIdAsync(string id);
        Task<User> FindUserByEmailAsync(string email);
        Task<bool> CheckIsEmailPresent(string email);
    }
}
