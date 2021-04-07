using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BookingApp.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AppContextDB _context;
        private readonly UserManager<User> _userManager;

        public BookingController(AppContextDB context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var appContextDB = _context.Booking.Include(b => b.Offer).Include(b => b.User);
            return View(await appContextDB.ToListAsync());
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Offer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("OfferId, ArrivalDate, ArrivalTime, DepartureDate, DepartureTime, NbPerson")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                int nbNight = (booking.DepartureDate - booking.ArrivalDate).Days;
                double pricePerNight = await _context.Offers.Where(o => o.Id == booking.OfferId).Select(o => o.PricePerNight).SingleOrDefaultAsync();
                double cleaningFee = await _context.Offers.Where(o => o.Id == booking.OfferId).Select(o => o.CleaningFee).SingleOrDefaultAsync();

                booking.TotalPrice = pricePerNight * (double)nbNight + cleaningFee;
                booking.UserId = (await _userManager.GetUserAsync(User)).Id;

                _context.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }


        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Offer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(Guid id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
    }
}
