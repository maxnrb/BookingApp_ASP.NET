using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Offer
    {
		public Guid Id { get; set; }

		//private User user;

		public Guid AccommodationId { get; set; }
		[Display(Name = "Logement")]
		public virtual Accommodation Accommodation { get; set; }

		[Display(Name = "Date d'ajout")]
		[DataType(DataType.DateTime)]
		public DateTime AddingDateTime { get; set; }

		[Display(Name = "Début disponibilité")]
		[DataType(DataType.Date)]
		public DateTime StartAvailability { get; set; }

		[Display(Name = "Fin disponibilité")]
		[DataType(DataType.Date)]
		public DateTime EndAvailability { get; set; }

		[Display(Name = "Prix par nuit")]
		public double PricePerNight { get; set; }

		[Display(Name = "Frais de ménage")]
		public double CleaningFee { get; set; }

		public Offer()
        {
			this.AddingDateTime = DateTime.Now;
        }
	}
}
