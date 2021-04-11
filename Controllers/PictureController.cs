using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Views.Accommodation
{
    [Route("Accommodation")]
    public class PictureController : Controller
    {
        private readonly AppContextDB _context;
        private readonly IWebHostEnvironment _environment;

        public PictureController(AppContextDB context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Accommodation/ManagePictures/5
        [Route("ManagePictures/{id:guid?}")]
        public async Task<IActionResult> ManagePictures(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .Include(a => a.Pictures)
                .Include(a => a.Rooms)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (accommodation == null)
            {
                return NotFound();
            }

            if (accommodation.Pictures.Count == 0)
            {
                ViewBag.AlertType = "warning";
                ViewBag.AlertMsg = "Veuillez ajouter au moins une photo à votre logement !";
            }

            if (TempData["AlertType"] != null && TempData["AlertMsg"] != null)
            {
                ViewBag.AlertType = TempData["AlertType"];
                ViewBag.AlertMsg = TempData["AlertMsg"];
            }

            return View(accommodation);
        }

        // POST: Accommodation/ManagePictures/5
        [HttpPost]
        [Route("ManagePictures/{id:guid?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePictures(Guid? id, List<IFormFile> files)
        {
            if (id == null)
            {
                return NotFound();
            }

            Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "upload"));

            foreach (var formFile in files)
            {
                if (await _context.Pictures.CountAsync(p => p.AccommodationId == (Guid)id) == 12)
                {
                    TempData["AlertType"] = "danger";
                    TempData["AlertMsg"] = "Vous avez atteint le nombre maximum de photos ! Une ou plusieurs photos n'ont pas pu être ajoutées.";
                    
                    return RedirectToAction("ManagePictures", new { id });
                }

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
        [Route("DeletePicture/{id:guid}")]
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
