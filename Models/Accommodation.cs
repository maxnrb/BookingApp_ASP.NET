using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
    public class Accommodation
    {
		// What is Name { get; set; }
		// https://stackoverflow.com/questions/5096926/what-is-the-get-set-syntax-in-c

		public Guid Id { get; set; }

		public string UserId { get; set; }

		[Display(Name = "Utilisateur")]
		public virtual User User { get; set; }

		[Display(Name = "Offres")]
		public virtual List<Offer> Offers { get; set; }

		[Display(Name = "Adresse")]
		public virtual Address Address { get; set; }

		[Display(Name = "Règlement Intérieur")]
		public virtual HouseRules HouseRules { get; set; }

		[Display(Name = "Photo(s)")]
		public virtual List<Picture> Pictures { get; set; }

		[Display(Name = "Pièce(s)")]
		public virtual List<Room> Rooms { get; set; }

		[Required(ErrorMessage = "Vous devez entrer un nom pour votre logement")]
		[Display(Name = "Nom")]
		public String Name { get; set; }

		[Required(ErrorMessage = "Vous devez sélectionner le type du logement")]
		[Display(Name = "Type")]
		[RegularExpression("Appartement|Maison|Chambre dans un appartement|Chambre dans une maison", 
			ErrorMessage = "Veuillez sélectionner un type de logement valide")]
		public String Type { get; set; }

		[Required(ErrorMessage = "Vous devez entrer la description de votre logement")]
		public String Description { get; set; }
	}
}
