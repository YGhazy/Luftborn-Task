
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DAL
{
    //ApplicationDbContext inherits from IdentityDbContext to implement Identity Tables. 
    //Reference the user class that inherits from IdentityUser class, Ex below : "ApplicationUser".

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<ConnectionId> ConnectionIds { get; set; }

    }
}
