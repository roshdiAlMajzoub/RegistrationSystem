using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Data;
using RegistrationSystem.Enums;
using RegistrationSystem.Models;
using System.Security.Cryptography;

namespace RegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<object> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return await Task.FromResult(new Response(ResponseCode.Error, "User already exists", null));
            }

            if (request.Password == request.ConfirmPassword)
            {

                CreatePasswordHash(request.Password,
                    out byte[] passwordHash,
                    out byte[] passwordSalt);

                var user = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Verified = "No",
                    declined = "No",
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return await Task.FromResult(new Response(ResponseCode.OK, "User Successfully Created", null));
            }
            else
                return await Task.FromResult(new Response(ResponseCode.Error, "Passwoard don't match", null));


        }


        [HttpPost("login")]
        public async Task<object> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return await Task.FromResult(new Response(ResponseCode.Error, "User not found.", null));
            }
            
            if(user.declined=="Yes")
            {
                return await Task.FromResult(new Response(ResponseCode.Error, "Declined from Registring", null));
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return await Task.FromResult(new Response(ResponseCode.Error, "Password incorrect", null));
            }

            if (user.Verified == "No")
            {
                return await Task.FromResult(new Response(ResponseCode.Error, "User not verified", null));

            }

            return await Task.FromResult(new Response(ResponseCode.OK, $"{user.Email}", null));

        }

        [HttpGet("users")]
        public async Task<object> getUsers()
        {
            var users = await _context.Users.Where(u => u.Email != "admin@admin.com" && u.declined=="No").ToListAsync();

            var viewUser = new List<ViewUser>();
            foreach (var user in users)
            {
                viewUser.Add(
                    new ViewUser()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Verified = user.Verified,
                    }
                );
            }
            return await Task.FromResult(new Response(ResponseCode.Error, "", viewUser));

        }



        [HttpPost("verify")]

        public async Task<object> Verify(VerifyRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (user == null)
            {
                return await Task.FromResult(new Response(ResponseCode.Error, "User not found.", null));
            }

            user.Verified = "Yes";
            _context.Entry(user).State = EntityState.Modified;

            _context.SaveChanges();

            return await Task.FromResult(new Response(ResponseCode.OK, "Verified", null));
        }



        [HttpPost("decline")]
        public async Task<object> Decline(VerifyRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.Id);
            if (user != null)
            {
                user.declined = "Yes";
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return await Task.FromResult(new Response(ResponseCode.OK, "Declined", null));
            }
            return await Task.FromResult(new Response(ResponseCode.Error, "Declined", null));

        }




        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
