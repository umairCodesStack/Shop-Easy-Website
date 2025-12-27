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
    public class UserRepository:IUserRepository
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
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public void UpdateUser(UpdateUserDTO user)
        {
            User newUser = new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
               
                PhoneNumber = user.PhoneNumber,
                imageUrl = user.ImageUrl
            };
            _context.Users.Update(newUser);
            _context.SaveChanges();
        }
        public bool EmailExists(string Email)
        {
            return _context.Users.Any(u => u.Email == Email);
        }
    }
   
}
