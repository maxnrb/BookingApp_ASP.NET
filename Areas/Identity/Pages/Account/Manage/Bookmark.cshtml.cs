using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApp.Controllers;
using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Areas.Identity.Pages.Account.Manage
{
    public partial class BookmarkModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly AppContextDB _context;

        public BookmarkModel(UserManager<User> userManager, AppContextDB context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<Bookmark> Bookmarks { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(User user)
        {
            Bookmarks = await _context.Bookmark
                .Where(b => b.UserId == user.Id)
                .Include(b => b.Offer)
                .Include(b => b.Offer.Accommodation)
                    .ThenInclude(a => a.Address)
                .Include(b => b.Offer.Accommodation)
                    .ThenInclude(a => a.Pictures)
                .Include(b => b.Offer.Accommodation.User)
                .ToListAsync();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteBookmarkAsync(Guid offerId)
        {
            await new BookmarkController(_context, _userManager.GetUserId(User)).Delete(offerId);

            return RedirectToPage();
        }
    }
}
