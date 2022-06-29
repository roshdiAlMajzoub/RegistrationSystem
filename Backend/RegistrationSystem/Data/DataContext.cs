
using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Data;
using RegistrationSystem.Models;

namespace RegistrationSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DataSeeder(modelBuilder).Seed();
        }



        public DbSet<User> Users => Set<User> ();
    }
}
