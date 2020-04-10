using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SftMembership.web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        
        public string Company { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password is mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string SubscriptionPlanName { get; set; }
        [Required]
        public DateTime Created { get; set; }

        public RegisterViewModel()
        {
            this.Created = DateTime.Now;
        }
    }
}
