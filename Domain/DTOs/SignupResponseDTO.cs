using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class SignupResponseDTO
    {
        public string userId { get; set; }
        public bool status { get; set; }
        public string message {  get; set; }
    }
}
