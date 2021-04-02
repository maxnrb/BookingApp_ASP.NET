using System;
using System.Collections.Generic;
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
        public virtual Accommodation Accommodation { get; set; }

        public RoomTypes RoomType { get; set; }
    }
}
