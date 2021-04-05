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
            switch(amenityType)
            {
                case "SingleBed": return "Lit simple";
                case "DoubleBed": return "Lit double";
                case "TV": return "Télévision";
                case "Closet": return "Penderie";
                case "Bathtub": return "Baignoire";
                case "Shower": return "Douche";
                case "WashingMachine": return "Machine à laver";
                case "Oven": return "Four";
                case "Freezer": return "Congélateur";
                case "CoffeeMaker": return "Machine à café";

                default: return null;
            }
        }
    }
}
