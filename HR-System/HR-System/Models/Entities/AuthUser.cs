﻿using Microsoft.AspNetCore.Identity;

namespace HR_System.Models.Entities
{
    public class AuthUser : IdentityUser
    {
        public bool IsApproved { get; internal set; }
    }
}
