using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EGWebApplication3.Models
{
    public class AppDbContext : IdentityDbContext
    {
         // create constructor
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
         {
    
         }

        // create property

        //We will use this DbSet property Employee to query and save instances of the Employee class
        public DbSet<Employee> Employees { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Seed();
        //}
    }


}
