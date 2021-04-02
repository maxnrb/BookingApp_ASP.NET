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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BookingApp.Controllers
{
    [Authorize]
    public class AccommodationController : Controller
    {
        private readonly AppContextDB _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _environment;

        public AccommodationController(AppContextDB context, UserManager<User> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
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
                .Include(a => a.Pictures)
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
            [Bind("StreetAndNumber, Complement, City, PostalCode, Country")] Address address,
            [Bind("ArrivalHour, DepartureHour, PetAllowed, PartyAllowed, SmokeAllowed")] HouseRules houseRules)
        {

            if (ModelState.IsValid)
            {
                accommodation.UserId = (await _userManager.GetUserAsync(User)).Id;
                accommodation.Address = address;
                accommodation.HouseRules = houseRules;
                
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
                .Include(a => a.HouseRules)
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
            [Bind("Id, StreetAndNumber, Complement, City, PostalCode, Country")] Address address,
            [Bind("Id, ArrivalHour, DepartureHour, PetAllowed, PartyAllowed, SmokeAllowed")] HouseRules houseRules)
        {
            if (id != accommodation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get accommodation's user
                accommodation.UserId = await _context.Accommodations.Where(a => a.Id == id).Select(a => a.UserId).SingleOrDefaultAsync();
                accommodation.Address = address;
                accommodation.HouseRules = houseRules;

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


        // GET: Accommodation/ManagePictures/5
        public async Task<IActionResult> ManagePictures(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .Include(a => a.Pictures)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // POST: Accommodation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePictures(Guid? id, List<IFormFile> files)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + "_" + Guid.NewGuid().ToString("N") 
                        + Path.GetExtension(formFile.FileName);

                    string filePath = Path.Combine(_environment.WebRootPath, "upload", fileName);

                    using var stream = System.IO.File.Create(filePath);
                    await formFile.CopyToAsync(stream);

                    await _context.Pictures.AddAsync(new Picture((Guid)id, fileName));
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("ManagePictures", new { id });
        }

        // GET: Accommodation/DeletePicture/5
        public async Task<IActionResult> DeletePicture(Guid id, Guid accommodationId)
        {
            // TODO: Check if user own the picture
            var picture = await _context.Pictures.FindAsync(id);

            string filePath = Path.Combine(_environment.WebRootPath, "upload", picture.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManagePictures", new { id = accommodationId });
        }
    }
}
