using System;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace AdminArea_IdentityBase.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}