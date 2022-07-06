using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Model;

namespace RestWithASPNET.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        protected MySqlContext()
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}