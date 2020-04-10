using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SftMembership.web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SftMembership.web.Persistance.Contexts
{
    public class SftDbContext : IdentityDbContext<User, Role,
                                int, IdentityUserClaim<int>,
                                UserRole, IdentityUserLogin<int>,
                                IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public SftDbContext(DbContextOptions<SftDbContext> options) : base(options) { }


        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            /*Configure Identity UserRole With Role entity
             * and User entity
             */
            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId).IsRequired();

                userRole.HasOne(ur => ur.User).WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId).IsRequired();


            });






            /* Configure SubscriptionPlan entity
             */
            modelBuilder.Entity<SubscriptionPlan>().HasKey(x => x.Id);
            modelBuilder.Entity<SubscriptionPlan>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<SubscriptionPlan>().Property(x => x.Name).IsRequired().HasMaxLength(20);



            modelBuilder.Entity<SubscriptionPlan>().HasData(
                new SubscriptionPlan { Id = 1, Name = "Solo" },
                new SubscriptionPlan { Id = 2, Name = "Multi User" }
                );

            modelBuilder.Entity<Role>().HasData(
               new Role { Id = 1, Name = "Admin" },
               new Role { Id = 2, Name = "Customer" }
               );









        }


    }
}
