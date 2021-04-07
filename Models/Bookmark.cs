using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Bookmark
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public Guid OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
