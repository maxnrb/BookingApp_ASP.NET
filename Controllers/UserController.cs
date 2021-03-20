using BookingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string UserId;

        public string UserEmail { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Vous devez entrer un prénom")]
            [Display(Name = "Prénom")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Vous devez entrer un nom")]
            [Display(Name = "Nom")]
            public string LastName { get; set; }

            [Required]
            [RegularExpression("User|Host|Admin", ErrorMessage = "Veuillez sélectionner un rôle valide")]
            [Display(Name = "Rôle")]
            public string Role { get; set; }
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound($"Id not specified");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{id}'");
            }

            UserEmail = user.Email;
            UserId = user.Id;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };

            return View(this);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String userId, IFormCollection collection)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserId}'");
            }

            if (ModelState.IsValid)
            {
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;

                string actualRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();


                if (actualRole == null)
                {
                    IdentityResult addingResult = await _userManager.AddToRoleAsync(user, Input.Role);
                }
                else if (!actualRole.Equals(Input.Role))
                {
                    IdentityResult deletionResult = await _userManager.RemoveFromRoleAsync(user, actualRole);
                    IdentityResult addingResult = await _userManager.AddToRoleAsync(user, Input.Role);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound($"Id not specified");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{id}'");
            }

            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound($"Id not specified");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{id}'");
            }

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
