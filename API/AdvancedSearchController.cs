using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvancedSearchController : ControllerBase
    {
        private readonly AppContextDB _context;

        public AdvancedSearchController(AppContextDB context)
        {
            _context = context;
        }

        // GET: api/<SearchController>
        [HttpGet("{city}/{arrivalDate}/{departureDate}/{nbPerson}")]
        public async Task<IEnumerable<Offer>> Get(string city, string arrivalDate, string departureDate, string nbPerson)
        {
            IEnumerable<Offer> offers = null;

            DateTime arrivalDateTime = DateTime.ParseExact(arrivalDate, "yyyy-MM-dd", null);
            DateTime departureDateTime = DateTime.ParseExact(departureDate, "yyyy-MM-dd", null);
            int nbPersonInt = int.Parse(nbPerson);

            if (arrivalDateTime < departureDateTime && !city.Equals(""))
            {
                 offers = await _context.Offers
                     .Where(o => o.StartAvailability <= arrivalDateTime && o.EndAvailability > arrivalDateTime && o.EndAvailability >= departureDateTime)
                     .Where(o => o.Accommodation.Address.City == city && o.Accommodation.MaxTraveler >= nbPersonInt)
                     .Include(o => o.Accommodation.Pictures)
                     .Include(o => o.Accommodation.Address)
                     .ToListAsync();
            }

            return offers;
        }
    }
}
