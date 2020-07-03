using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MyDayApp.BusinessLogic.AccountLogic.Interfaces;
using MyDayApp.Models;

namespace MyDayApp.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        /// <summary>
        /// These SignIn field is for logging in and creating users using the identity API
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogoutLogic _logoutLogic;
        private readonly IRegisterLogic _registerLogic;
        private readonly ILoginLogic _loginLogic;


        public AccountController(
            RoleManager<IdentityRole> roleManager,
            ILogoutLogic logoutLogic,
            IRegisterLogic registerLogic,
            ILoginLogic loginLogic)
        {
            this._roleManager = roleManager;
            this._logoutLogic = logoutLogic;
            this._registerLogic = registerLogic;
            this._loginLogic = loginLogic;
        }
        
        /// <summary>
        /// Logout button by header section
        /// </summary>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
             _logoutLogic.Logout();
             return RedirectToAction("Login", "Account");
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }


        //checks whether the correct combination of the entered email address and passwords are correct
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, PasswordHash = model.Password, };
                await _loginLogic.Login(user);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Email en/of Wachtwoord is incorrect. Probeer het opnieuw.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var account = new RegisterViewModel
            {
                RoleItems = _roleManager.Roles.Select(iR => new SelectListItem
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
                await _registerLogic.Register(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}