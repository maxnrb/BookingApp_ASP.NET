using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Booking
    {
        public Guid Id { get; set; }

        public Guid OfferId { get; set; }

        [Display(Name = "Offre")]
        public virtual Offer Offer { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Utilisateur")]
        public virtual User User { get; set; }

        [Display(Name = "Date de réservation")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDateTime { get; set; }

        [Display(Name = "Date d'arrivé")]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "Horaire d'arrivé")]
        [DataType(DataType.Time)]
        public TimeSpan ArrivalTime { get; set; }

        [Display(Name = "Date de départ")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Display(Name = "Horaire de départ")]
        [DataType(DataType.Time)]
        public TimeSpan DepartureTime { get; set; }

        [Display(Name = "Nombre de voyageur(s)")]
        public int NbPerson { get; set; }

        [Display(Name = "Prix total")]
        public double TotalPrice { get; set; }

        public Booking()
        {
            this.BookingDateTime = DateTime.Now;
        }
	}
}
