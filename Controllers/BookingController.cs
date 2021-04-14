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
            User user = await _userManager.GetUserAsync(User);

            if (user == null) { return NotFound(); }

            ViewBag.ReturnAction = "Index";

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return View(await _context.Booking
                    .Include(b => b.Offer).Include(b => b.User).Include(b => b.Offer.Accommodation).ToListAsync());
            }
            else
            {
                return View(await _context.Booking
                    .Where(b => b.UserId == user.Id)
                    .Include(b => b.Offer).Include(b => b.User).Include(b => b.Offer.Accommodation).ToListAsync());
            }
        }

        // GET: Booking/HostIndex
        [Authorize(Roles = "Host, Admin")]
        public async Task<IActionResult> HostIndex()
        {
            User user = await _userManager.GetUserAsync(User);

            if (user == null) { return NotFound(); }

            ViewBag.ReturnAction = "HostIndex";

            return View("Index", await _context.Booking
                .Include(b => b.Offer).Include(b => b.User).Include(b => b.Offer.Accommodation)
                .Where(b => b.Offer.Accommodation.UserId == user.Id).ToListAsync());
        }


        // POST: Booking/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Guid id, string returnAction)
        {
            var booking = await _context.Booking
                .Include(b => b.Offer)
                .Include(b => b.User)
                .Include(b => b.Offer.Accommodation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            ViewBag.ReturnAction = returnAction;

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

                User senderUser = await _userManager.GetUserAsync(User);
                User receiverUser = await _context.Offers.Where(o => o.Id == booking.OfferId).Select(o => o.Accommodation.User).SingleOrDefaultAsync();

                // Calcul total price
                double totalPrice = pricePerNight * (double)nbNight + cleaningFee;

                if (await new TransactionController(_context).DoTransaction(senderUser, receiverUser, totalPrice))
                {
                    booking.TotalPrice = totalPrice;
                    booking.UserId = (await _userManager.GetUserAsync(User)).Id;

                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    TempData["AlertType"] = "danger";
                    TempData["AlertMsg"] = "Vous n'avez pas le montant nécessaire pour procéder à la réservation, il vous manque " +  (totalPrice-senderUser.Balance) + " € ! Veuillez <a href=\"/Identity/Account/Manage/Wallet\">créditer votre compte</a>";

                    return RedirectToAction("View", "Offer", new { id = booking.OfferId });
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
