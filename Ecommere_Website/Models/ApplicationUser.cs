﻿using Microsoft.AspNetCore.Identity;

namespace Ecommere_Website.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name {  get; set; }
    }
}
