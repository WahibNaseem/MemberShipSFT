using Microsoft.AspNetCore.Identity;
using SftMembership.web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SftMembership.web.Persistance.Contexts
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {

                var roles = new List<Role>
                {
                    new Role { Name = "Admin"},
                    new Role { Name = "Customer"}
                };

                var adminUser = new User
                {
                    UserName = "Admin",
                    FirstName = "Wahib",
                    LastName = "Naseem",
                };

                foreach (var role in roles)
                    _roleManager.CreateAsync(role).Wait();

                _roleManager.Dispose();

                IdentityResult result = _userManager.CreateAsync(adminUser, "root").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRoleAsync(admin, "admin").Wait();
                }
            }
        }
    }
}
