using Microsoft.EntityFrameworkCore;
using WEBApi.Entities;

namespace WEBApi.Data
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
