using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Repositories;
using PersonalFinanceTracker.ViewModels;

namespace PersonalFinanceTracker.Services
{
    /// <summary>
    /// Authentication Service Interface
    /// Handles user authentication and session management
    /// </summary>
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        bool Register(RegisterViewModel model);
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        bool UserExists(string username, string email);
    }

    /// <summary>
    /// Authentication Service Implementation
    /// Implements security and authentication logic
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Authenticate(string username, string password)
        {
            var user = _unitOfWork.Users.FirstOrDefault(u => u.Username == username);
            
            if (user == null)
                return null;

            // Verify password hash
            if (!VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }

        public bool Register(RegisterViewModel model)
        {
            try
            {
                // Check if user already exists
                if (UserExists(model.Username, model.Email))
                    return false;

                // Hash the password
                var passwordHash = HashPassword(model.Password);

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    CreatedDate = DateTime.Now
                };

                _unitOfWork.Users.Add(user);
                return _unitOfWork.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public User GetUserById(int userId)
        {
            return _unitOfWork.Users.GetById(userId);
        }

        public User GetUserByUsername(string username)
        {
            return _unitOfWork.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool UserExists(string username, string email)
        {
            return _unitOfWork.Users.Any(u => u.Username == username || u.Email == email);
        }

        /// <summary>
        /// Hash password using SHA256
        /// In production, use BCrypt or PBKDF2
        /// </summary>
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Verify password against hash
        /// </summary>
        private bool VerifyPassword(string password, string hash)
        {
            var passwordHash = HashPassword(password);
            return passwordHash == hash;
        }
    }
}
