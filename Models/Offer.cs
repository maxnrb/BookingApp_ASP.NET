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
		public virtual Accommodation Accommodation { get; set; }

		[Display(Name = "Date d'ajout")]
		[DataType(DataType.Date)]
		public DateTime AddingDate { get; set; }

		[Display(Name = "Début disponibilité")]
		[DataType(DataType.Date)]
		public DateTime StartAvailability { get; set; }

		[Display(Name = "Fin disponibilité")]
		[DataType(DataType.Date)]
		public DateTime EndAvailability { get; set; }

		public double PricePerNight { get; set; }

		public double CleaningFee { get; set; }
	}
}
