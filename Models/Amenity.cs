using Microsoft.EntityFrameworkCore;
using System;

namespace BookingApp.Models
{
    public enum AmenityTypes
    {
        SingleBed,
        DoubleBed,
        TV,
        Closet,
        Bathtub,
        Shower,
        WashingMachine,
        Oven,
        Freezer,
        CoffeeMaker
    }

    public class Amenity
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public AmenityTypes AmenityType { get; set; }

        public override string ToString()
        {
            return ToFr(Enum.GetName(typeof(AmenityTypes), this.AmenityType));
        }

        public string ToFr(string amenityType)
        {
            return amenityType switch
            {
                "SingleBed" => "Lit simple",
                "DoubleBed" => "Lit double",
                "TV" => "Télévision",
                "Closet" => "Penderie",
                "Bathtub" => "Baignoire",
                "Shower" => "Douche",
                "WashingMachine" => "Machine à laver",
                "Oven" => "Four",
                "Freezer" => "Congélateur",
                "CoffeeMaker" => "Machine à café",
                _ => null,
            };
        }
    }
}
