using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDayApp.Models;

namespace MyDayApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Deze functie creeërt een nieuwe identity als de modelState goed verwerkt is
                var user = new IdentityUser { UserName = model.FirstName + model.LastName, Email = model.Email };
                //Vanaf hier kijkt het programma of alle ingevoerde waardes goed gecreeërd kunnen worden
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Zoja, dan wordt je automatisch ingelogd
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                //Zo niet, dan haalt hij alle errors op en laat ze zien
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {


                //Vanaf hier kijkt het programma of alle ingevoerde waardes goed gecreeërd kunnen worden
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Email/Password combination is incorrect");
            }
            return View(model);
        }
    }
}