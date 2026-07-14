using CinemaMont.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaMont.Services
{
    public interface IPasswordHelper
    {
        public string Encrypt(User user, string initialPassword);
    }

    public class PasswordEncrypt : IPasswordHelper
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public PasswordEncrypt(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Encrypt(User user, string initialPassword)
        {
            return _passwordHasher.HashPassword(user, initialPassword);
        }
    }
}
