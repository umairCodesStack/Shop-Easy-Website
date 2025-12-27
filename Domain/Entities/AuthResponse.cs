using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string AccessToken {get; set; }
        public string TokenType { get; set; }        
        public string Username {get; set; }
         public string Email { get; set; }   
         public string   Role { get; set; }
    }
}
