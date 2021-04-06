using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public enum RoomTypes
    {
        Bedroom,
        Bathroom,
        Kitchen
    }

    public class Room
    {
        public Guid Id { get; set; }

        public Guid AccommodationId { get; set; }

        public RoomTypes RoomType { get; set; }

        [Display(Name = "Equipement(s)")]
        public virtual List<Amenity> Amenities { get; set; }

        public string AmenitiesStr()
        {
            return String.Join(", ", Amenities);
        }
    }

    public abstract class TypeTranslate
    {
        public static string ToFr(string roomType)
        {
            return roomType switch
            {
                "Bedroom" => "Chambre",
                "Bathroom" => "Salle de bain",
                "Kitchen" => "Cuisine",
                _ => null,
            };
        }
    }
}
