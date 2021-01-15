using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
{
    public interface IUserAddRepo
    {
        Task InsertUserIntoTheDb(User user);
    }
}
