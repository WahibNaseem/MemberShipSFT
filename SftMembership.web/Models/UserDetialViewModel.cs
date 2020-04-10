using SftMembership.web.Domain.Models;
using System;

namespace SftMembership.web.Models
{
    public class UserDetialViewModel
    {

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public DateTime Created { get; set; }
        public  SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
