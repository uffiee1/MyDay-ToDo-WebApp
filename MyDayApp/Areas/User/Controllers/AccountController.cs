using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MyDayApp.Models;

namespace MyDayApp.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        /// <summary>
        /// These SignIn field is for logging in and creating users using the identity API
        /// </summary>
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        
        /// <summary>
        /// Logout button by header section
        /// </summary>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        //checks whether the correct combination of the entered email address and passwords are correct
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
               // var user = await userManager.FindByEmailAsync(model.Email);
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Profile", "Dashboard");   
                    }
                }
                ModelState.AddModelError(string.Empty, "Email en/of Wachtwoord is incorrect. Probeer het opnieuw.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {

            var account = new RegisterViewModel
            {
                RoleItems = roleManager.Roles.Select(iR => new SelectListItem
                {
                    Text = iR.Name,
                    Value = iR.Name
                })
            };
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email, };
                var result = await userManager.CreateAsync((User)user, model.Password);

                //IdentityRole identityRole = new IdentityRole
                //{
                //    Name = model.RoleName
                //};

                //result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(Role.Gebruiker))
                    {
                        await roleManager.CreateAsync(new IdentityRole(Role.Gebruiker));
                    }

                    if (!await roleManager.RoleExistsAsync(Role.Administrator))
                    {
                        await roleManager.CreateAsync(new IdentityRole(Role.Administrator));
                    }

                    if (model.RoleName == null)
                    {
                        await userManager.AddToRoleAsync(user, Role.Gebruiker);
                    }

                    await userManager.AddToRoleAsync(user, model.RoleName);

                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}