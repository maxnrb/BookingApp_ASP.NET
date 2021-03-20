using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookingApp.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [PersonalData]
        public String FirstName { get; set; }

        [PersonalData]
        public String LastName { get; set; }

        [PersonalData]
        public Double Balance { get; set; }

        [Display(Name = "Logement")]
        public virtual List<Accommodation> Accommodations { get; set; }
    }
}
