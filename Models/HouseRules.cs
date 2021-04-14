using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class HouseRules
    {
        [ForeignKey("Accommodation")]
        public Guid Id { get; set; }

        public virtual Accommodation Accommodation { get; set; }

        [Display(Name = "Heure minimum d'arrivée")]
        [Required(ErrorMessage = "Vous devez indiquer l'heure minimum d'arrivée")]
        [DataType(DataType.Time)]
        public TimeSpan ArrivalHour { get; set; }

        [Display(Name = "Heure maximum de départ")]
        [Required(ErrorMessage = "Vous devez indiquer l'heure maximum de départ")]
        [DataType(DataType.Time)]
        public TimeSpan DepartureHour { get; set; }

        [Display(Name = "Animaux autorisés")]
        public bool PetAllowed { get; set; }

        [Display(Name = "Fêtes autorisées")]
        public bool PartyAllowed { get; set; }

        [Display(Name = "Logement fumeur")]
        public bool SmokeAllowed { get; set; }

        public override string ToString()
        {
            return "Animaux : " + (PetAllowed ? "Oui" : "Non") + " -- Fête : " + (PartyAllowed ? "Oui" : "Non") + " -- Fumeur : " + (SmokeAllowed ? "Oui" : "Non")
                + " || Heure arrivée : " + ArrivalHour.ToString("hh\\hmm") + " -- Heure départ : " + DepartureHour.ToString("hh\\hmm");
        }
    }
}
