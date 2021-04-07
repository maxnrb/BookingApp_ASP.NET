using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly AppContextDB _context;

        public BookmarkController(AppContextDB context)
        {
            _context = context;
        }

        // POST: BookmarkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Add(Guid offerId, string userId)
        {
            if (userId != null && BookmarkExists(offerId, userId) == null)
            {
                Bookmark bookmark = new();
                bookmark.OfferId = offerId;
                bookmark.UserId = userId;

                await _context.Bookmark.AddAsync(bookmark);
                await _context.SaveChangesAsync();
            }
        }

        // POST: BookmarkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Delete(Guid offerId, string userId)
        {
            var bookmark = BookmarkExists(offerId, userId);

            if (bookmark != null)
            {
                _context.Bookmark.Remove(bookmark);
                await _context.SaveChangesAsync();
            }
        }

        private Bookmark BookmarkExists(Guid offerId, string userId)
        {
            return _context.Bookmark.Where(b => b.OfferId == offerId && b.UserId == userId).SingleOrDefault();
        }
    }
}
