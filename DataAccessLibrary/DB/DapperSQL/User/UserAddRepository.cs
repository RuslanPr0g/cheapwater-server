
using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DB
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
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
