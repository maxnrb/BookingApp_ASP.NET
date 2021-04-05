using System;

namespace BookingApp.Models
{
    public class Picture
    {
        public Guid Id { get; set; }

        public Guid AccommodationId { get; set; }

        public String FileName { get; set; }

        public Picture(Guid accommodationId, String fileName)
        {
            this.AccommodationId = accommodationId;
            this.FileName = fileName;
        }
    }
}
