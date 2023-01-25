using ITnCare.CM.Entities;
using ITnCare.Service.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ITnCare.CM
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(IOptions<GlobalOptions> options) : base(GetOptions(options.Value.ConnectionStrings["SqlServerConnectionString"]))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
