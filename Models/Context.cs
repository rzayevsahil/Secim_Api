using Microsoft.EntityFrameworkCore;

namespace Secim_Api.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Electric> Electrics { get; set; }
    }
}
