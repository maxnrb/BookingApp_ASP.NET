using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookingApp.Data
{
    public class AppContextDB : IdentityDbContext<User>
    {
        public AppContextDB(DbContextOptions<AppContextDB> options) : base(options) { }

        public DbSet<Accommodation> Accommodations { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Address> Address { get; set; }
    }
}
