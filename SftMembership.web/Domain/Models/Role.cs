
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SftMembership.web.Domain.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}