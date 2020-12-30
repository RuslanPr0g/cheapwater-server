using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public interface IUserReadRepository
    {
        Task<User> FindUserByIdAsync(string id, CancellationToken cancellation);
        Task<User> FindUserByEmailAsync(string email, CancellationToken cancellation);
        Task<bool> CheckIsEmailPresent(string email, CancellationToken cancellation);
    }
}
