using api_rest_with_aspnet_10.Models;
using Microsoft.EntityFrameworkCore;

namespace api_rest_with_aspnet_10.Context
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; } // Desafio
    }
}
