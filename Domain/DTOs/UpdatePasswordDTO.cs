using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UpdatePasswordDTO
    {
        public string email { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
