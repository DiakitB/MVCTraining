using believe.Models;
using Microsoft.EntityFrameworkCore;

namespace believe.DataOD
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Category> categories { get; set; }
        public DbSet<CoverType> coverType { get; set; }
        public DbSet<Product> products { get; set; }

    }
}
