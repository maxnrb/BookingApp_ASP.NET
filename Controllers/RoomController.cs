using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppContextDB _context;

        public RoomController(AppContextDB context)
        {
            _context = context;
        }

        // GET: Accommodation/ManageRooms/5
        [Route("Accommodation/ManageRooms/{id?}")]
        public async Task<IActionResult> ManageRooms(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodations
                .Include(a => a.Pictures)
                .Include(a => a.Rooms)
                .ThenInclude(r => r.Amenities)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (accommodation == null)
            {
                return NotFound();
            }

            if (accommodation.Rooms.Count == 0)
            {
                ViewBag.AlertType = "warning";
                ViewBag.AlertMsg = "Veuillez ajouter au moins une pièce à votre logement !";
            }

            return View(accommodation);
        }


        // POST: Accommodation/ManageRooms/5
        [HttpPost]
        [Route("Accommodation/ManageRooms/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRooms(Guid? accommodationId, string roomType,
            string singleBedNumber, string doubleBedNumber, string tv, string closet,
            string bathtub, string shower, string washingMachine,
            string oven, string freezer, string coffeeMaker)
        {
            if (accommodationId == null)
            {
                return NotFound();
            }

            List<Amenity> amenities = new();

            switch (roomType)
            {
                case "Bedroom":
                    if (tv == null && closet == null && (singleBedNumber == null || int.Parse(singleBedNumber) == 0) && (doubleBedNumber == null || int.Parse(doubleBedNumber) == 0))
                    {
                        ViewBag.AlertType = "danger";
                        ViewBag.AlertMsg = "Veuillez ajouter au moins un équipement à votre pièce !";
                    }
                    else
                    {
                        AddMultipleAmenities(amenities, singleBedNumber, "SingleBed");
                        AddMultipleAmenities(amenities, doubleBedNumber, "DoubleBed");

                        AddAmenityIfChecked(amenities, tv, "TV");
                        AddAmenityIfChecked(amenities, closet, "Closet");

                        await SaveRoom((Guid)accommodationId, roomType, amenities);
                    }

                    break;

                case "Bathroom":
                    if (bathtub == null && shower == null && washingMachine == null)
                    {
                        ViewBag.AlertType = "danger";
                        ViewBag.AlertMsg = "Veuillez ajouter au moins un équipement à votre pièce !";
                    } 
                    else
                    {
                        AddAmenityIfChecked(amenities, bathtub, "Bathtub");
                        AddAmenityIfChecked(amenities, shower, "Shower");
                        AddAmenityIfChecked(amenities, washingMachine, "WashingMachine");

                        await SaveRoom((Guid)accommodationId, roomType, amenities);
                    }

                    break;

                case "Kitchen":
                    if (oven == null && freezer == null && coffeeMaker == null)
                    {
                        ViewBag.AlertType = "danger";
                        ViewBag.AlertMsg = "Veuillez ajouter au moins un équipement à votre pièce !";
                    } 
                    else
                    {
                        AddAmenityIfChecked(amenities, oven, "Oven");
                        AddAmenityIfChecked(amenities, freezer, "Freezer");
                        AddAmenityIfChecked(amenities, coffeeMaker, "CoffeeMaker");

                        await SaveRoom((Guid)accommodationId, roomType, amenities);
                    }

                    break;
            }

            return RedirectToAction("ManageRooms", new { id = accommodationId });
        }

        private async Task SaveRoom(Guid accommodationId, string roomType, List<Amenity> amenities)
        {
            Room room = new()
            {
                AccommodationId = accommodationId,
                RoomType = (RoomTypes)Enum.Parse(typeof(RoomTypes), roomType, true),
                Amenities = amenities
            };

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        private static void AddAmenityIfChecked(List<Amenity> amenities, string amenity, string name)
        {
            if (amenity != null && amenity.Equals("on"))
            {
                amenities.Add(new Amenity { AmenityType = (AmenityTypes)Enum.Parse(typeof(AmenityTypes), name, true) });
            }
        }

        private static void AddMultipleAmenities(List<Amenity> amenities, string amenityNb, string name)
        {
            if (amenityNb != null)
            {
                for (int i = 0; i < int.Parse(amenityNb); i++)
                {
                    amenities.Add(new Amenity { AmenityType = (AmenityTypes)Enum.Parse(typeof(AmenityTypes), name, true) });
                }
            }
        }

        // GET: Accommodation/DeletePicture/5
        [Route("Accommodation/DeleteRoom/{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id, Guid accommodationId)
        {
            // TODO: Check if user own the picture
            var room = await _context.Rooms.FindAsync(id);

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageRooms", new { id = accommodationId });
        }
    }
}
