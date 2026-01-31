using DocumentsAPI.Core.Users.Interfaces;
using DocumentsAPI.Core.Users.Models;
using DocumentsAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DocumentsAPI.Core.Users.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IJwtTokenGenerator _jwt;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<string> SignInAsync(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);


            if (user == null || !VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _jwt.Generate(user);
        }

        public async Task<string> SignUpAsync(string email, string password, string name)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == email);
            if (exists)
                throw new Exception("User already exists");


            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = HashPassword(password),
                Name = name
            };


            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return _jwt.Generate(user);
        }

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        private bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
