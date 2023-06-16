using InsuranceCompnay.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompnay.DataAccess
{
    public class ApiDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();


            base.OnModelCreating(modelBuilder);
        }
    }
}
