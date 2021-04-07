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
        [Display(Name = "Prénom")]
        public String FirstName { get; set; }

        [PersonalData]
        [Display(Name = "Nom")]
        public String LastName { get; set; }

        [PersonalData]
        [Display(Name = "Solde")]
        public Double Balance { get; set; }

        [Display(Name = "Logement(s)")]
        public virtual List<Accommodation> Accommodations { get; set; }

        [Display(Name = "Favoris")]
        public virtual List<Bookmark> Bookmarks { get; set; }
    }
}
