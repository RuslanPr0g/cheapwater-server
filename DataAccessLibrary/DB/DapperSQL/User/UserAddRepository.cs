
using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB.DapperSQL
{
    public class UserAddRepository:IUserAddRepository
    {
        private readonly AuthContext _context;

        public UserAddRepository(AuthContext context)
        {
            this._context = context;
        }

        public async Task InsertUserIntoTheDb(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
