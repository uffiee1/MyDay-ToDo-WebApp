using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDayApp.Areas.User.Models;
using MyDayApp.Models;
using UserViewModel = MyDayApp.Areas.User.Models.UserViewModel;

namespace MyDayApp.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {
        private readonly UserManager<MyDayApp.Models.User> userManager;

        public ProfileController(UserManager<MyDayApp.Models.User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(string id)
        {
            var user = await userManager.GetUserAsync(User);
            //var Email = await userManager.GetEmailAsync(user);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return NotFound();
            }

            // GetClaimsAsync retunrs the list of user Claims

            var model = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Surname = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.PasswordHash,
                Age = user.DateOfBirth
                
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = await userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.Surname;
                user.Email = model.Email;
                user.PasswordHash = model.Password;
                user.DateOfBirth = model.Age;
                
                
                var result = await userManager.UpdateAsync(user);
        
                if (result.Succeeded)
                {
                    return RedirectToAction("EditProfile");
                }
        
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            return View(model);
        }
    }
}
