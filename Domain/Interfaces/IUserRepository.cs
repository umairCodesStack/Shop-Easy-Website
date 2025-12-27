using Azure.Core;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public User AddUser(AddUserDTO user);
        public void DeleteUser(int userId);
        public void UpdateUser(UpdateUserDTO user);
        public bool EmailExists(string email);
        public User? GetUserByEmail(string email);
    }
}
