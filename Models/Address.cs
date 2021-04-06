using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BookingApp.Models
{
    public class Address
    {
		[ForeignKey("Accommodation")]
		public Guid Id { get; set; }

		[JsonIgnore] 
		public virtual Accommodation Accommodation { get; set; }

		[Required(ErrorMessage = "Vous devez entrer le N° et la rue de votre logement")]
		[Display(Name = "N° et Rue")]
		public String StreetAndNumber { get; set; }

		[Display(Name = "Complément d'adresse")]
		public String Complement { get; set; }

		[Required(ErrorMessage = "Vous devez entrer le code postal de votre logement")]
		[Display(Name = "Code postal")]
		public String PostalCode { get; set; }

		[Required(ErrorMessage = "Vous devez entrer la ville de votre logement")]
		[Display(Name = "Ville")]
		public String City { get; set; }

		[Required(ErrorMessage = "Vous devez entrer le pays de votre logement")]
		[Display(Name = "Pays")]
		public String Country { get; set; }

		public override String ToString()
        {
			return StreetAndNumber + ", " + Complement + "\n" + PostalCode + " " + City + ", " + Country;
        }

		public String ShortAddress()
        {
			return City + ", " + Country;
		}
	}
}
