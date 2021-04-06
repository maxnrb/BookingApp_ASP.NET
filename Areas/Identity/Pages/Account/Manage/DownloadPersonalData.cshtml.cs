using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingApp.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly AppContextDB _context;

        public DownloadPersonalDataModel(
            UserManager<User> userManager,
            ILogger<DownloadPersonalDataModel> logger,
            AppContextDB context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            //// Only include personal data for download
            //var personalData = new Dictionary<string, string>();
            //var personalDataProps = typeof(User).GetProperties().Where(
            //                prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            //foreach (var p in personalDataProps)
            //{
            //    personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            //}

            //var logins = await _userManager.GetLoginsAsync(user);
            //foreach (var l in logins)
            //{
            //    personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            //}

            string userId = (await _userManager.GetUserAsync(User)).Id;

            var userDetails = await _context.Users
                .Include(u => u.Accommodations)
                //.ThenInclude(a => a.Address)
                .SingleOrDefaultAsync(u => u.Id == userId);

            userDetails.Id = null;
            userDetails.PasswordHash = null;
            userDetails.SecurityStamp = null;
            userDetails.ConcurrencyStamp = null;
            userDetails.AccessFailedCount = -1;

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(userDetails), "application/json");
        }
    }
}
