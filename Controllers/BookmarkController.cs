using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    [Authorize]
    public class BookmarkController : Controller
    {
        private readonly AppContextDB _context;
        private readonly string _userId;

        public BookmarkController(AppContextDB context, string userId)
        {
            _context = context;
            _userId = userId;
        }

        public async Task Add(Guid offerId)
        {
            // Check if bookmark already exist for actual connected user
            if (BookmarkExists(offerId) == null)
            {
                Bookmark bookmark = new();
                bookmark.OfferId = offerId;
                bookmark.UserId = _userId;

                await _context.Bookmark.AddAsync(bookmark);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid offerId)
        {
            var bookmark = BookmarkExists(offerId);

            if (bookmark != null)
            {
                _context.Bookmark.Remove(bookmark);
                await _context.SaveChangesAsync();
            }
        }

        // Check if bookmark already exist for actual connected user
        // Return the bookmark if it exists
        private Bookmark BookmarkExists(Guid offerId)
        {
            return _context.Bookmark.Where(b => b.OfferId == offerId && b.UserId == _userId).SingleOrDefault();
        }
    }
}
