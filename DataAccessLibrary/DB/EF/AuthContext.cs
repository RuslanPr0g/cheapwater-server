using DataAccessLibrary.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.DB
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
