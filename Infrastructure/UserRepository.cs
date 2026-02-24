using Azure.Core;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public User AddUser(AddUserDTO user)
        {
            User newUser = new User
            {
                Name = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                imageUrl = user.ImageUrl
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;

        }
        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public User? GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public int DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                return _context.SaveChanges();
            }
            return 0; // No user found to delete
        }
        public int UpdateUser(UpdateUserDTO user)
        {
            try
            {
                // Fetch the existing user from database
                var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

                if (existingUser == null)
                {
                    throw new Exception($"User with ID {user.Id} not found");
                }

                // Only update fields that are provided (not null or empty)
                if (!string.IsNullOrWhiteSpace(user.Name))
                {
                    existingUser.Name = user.Name;
                }

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    existingUser.Email = user.Email;
                }

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                {
                    existingUser.PhoneNumber = user.PhoneNumber;
                }

                // For imageUrl, allow explicit empty string to remove image
                if (user.ImageUrl != null)
                {
                    existingUser.imageUrl = user.ImageUrl;
                }

                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user: " + ex.Message);
                throw;
            }
        }
        public bool ChangePassword(UpdatePasswordDTO update)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == update.email);

            if (user == null)
                return false;

            // Verify current password
            if (!BCrypt.Net.BCrypt.Verify(update.currentPassword, user.Password))
                return false;

            // Hash and save new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(update.newPassword);
            _context.SaveChanges();

            return true;
        }
        public bool EmailExists(string Email)
        {
            return _context.Users.Any(u => u.Email == Email);
        }
    }

}
