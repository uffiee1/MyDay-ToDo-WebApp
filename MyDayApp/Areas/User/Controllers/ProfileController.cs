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
        private readonly UserManager<IdentityUser> userManager;

        public ProfileController(UserManager<IdentityUser> userManager)
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
                //Firstname = user.Firstname,
                //Surname = user.Surname,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.PasswordHash
                
                //Age = user.Age
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
                //user.Firstname = model.Firstname;
                //user.SurName = model.Surname;
                user.Email = model.Email;
                //user.Age = model.Age;
                user.PasswordHash = model.Password;
                
                
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
