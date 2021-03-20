using System;
using System.Collections.Generic;
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
    public class AccommodationController : Controller
    {
        private readonly AppContextDB _context;
        private readonly UserManager<User> _userManager;

        public AccommodationController(AppContextDB context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Accommodation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accommodations.Include(a => a.Address).Include(a => a.User).ToListAsync());
        }

        // GET: Accommodation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                // Include accommodation offers and address
                .Include(a => a.Offers)
                .Include(a => a.Address)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (accommodation == null) {
                return NotFound();
            }

            return View(accommodation);
        }

        // GET: Accommodation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accommodation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name, Type, Description")] Accommodation accommodation, 
            [Bind("StreetAndNumber, Complement, City, PostalCode, Country")] Address address)
        {
            accommodation.Address = address;
            accommodation.UserId = (await _userManager.GetUserAsync(User)).Id;

            if (ModelState.IsValid)
            {
                _context.Add(accommodation);
                await _context.SaveChangesAsync();

                // Redirect to controller index
                return RedirectToAction(nameof(Index));
            }
            return View(accommodation);
        }

        // GET: Accommodation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .Include(a => a.Address)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (accommodation == null)
            {
                return NotFound();
            }
            return View(accommodation);
        }

        // POST: Accommodation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, 
            [Bind("Id, Name, Type, Description")] Accommodation accommodation,
            [Bind("Id, StreetAndNumber, Complement, City, PostalCode, Country")] Address address)
        {
            if (id != accommodation.Id)
            {
                return NotFound();
            }

            accommodation.Address = address;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accommodation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccommodationExists(accommodation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accommodation);
        }

        // GET: Accommodation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // POST: Accommodation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);
            _context.Accommodations.Remove(accommodation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccommodationExists(Guid id)
        {
            return _context.Accommodations.Any(e => e.Id == id);
        }
    }
}
