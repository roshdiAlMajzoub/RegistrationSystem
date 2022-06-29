using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Models;
using System.Security.Cryptography;

namespace RegistrationSystem.Data
{
    public class DataSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public DataSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        //seeding data when ,igration for the admin account.
        public void Seed()
        {
            CreatePasswordHash("admin123",
                out byte[] passwordHash,
                out byte[] passwordSalt);

            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(k => k.Id);
                u.HasData(new User()
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Verified = "Yes",
                    declined = "No",
                });
            });
        }

        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

