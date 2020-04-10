using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SftMembership.web.Domain.Models
{
    public class User : IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public virtual SubscriptionPlan SubscriptionPlan { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
