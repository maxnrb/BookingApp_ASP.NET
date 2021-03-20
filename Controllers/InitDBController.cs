using BookingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    public class InitDBController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public InitDBController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            string returnText = "";

            if (_roleManager.Roles.Count() == 0)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Host" });
                await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                returnText += "Roles created<br/>";
            } 
            else
            {
                returnText += "Roles already created<br/>";
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                returnText += "Not connected. Can't add as Admin<br/>";
            }
            else if (!await _userManager.IsInRoleAsync(user, "Admin") )
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                await _signInManager.RefreshSignInAsync(user);
                returnText += "Actual user set as Admin<br/>";
            }
            else
            {
                returnText += "Actual user already set as Admin<br/>";
            }

            return base.Content(returnText, "text/html");
        }
    }
}
