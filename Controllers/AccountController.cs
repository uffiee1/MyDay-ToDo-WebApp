using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDayApp.Data;
using MyDayApp.Models;

namespace MyDayApp.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// These SignIn field is for logging in and creating users using the identity API
        /// </summary>
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Logout button by header section
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// checks whether the correct combination of the entered email address and passwords are correct
        /// 
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError(string.Empty, "Email en/of Wachtwoord is incorrect. Probeer het opnieuw.");
            }

            return View(model);
        }

<<<<<<< HEAD
<<<<<<< HEAD




=======




>>>>>>> parent of 4c9f086... Completed Login Section
=======




>>>>>>> parent of 4c9f086... Completed Login Section
        //Registration Action
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        ///
        ///Hier onder heb ik mijn code als command gezet omdat het nog niet klaar was en nog errors geeft.
        /// Voor nu heb ik normale regsiter pagina. (Is er wel maar werkt niet omdat er geen post actie is.) - (Zie boven)
        /// Code die onder ligt is als extra features zoals email verificatie,
        /// wachtwoorden automastish hashen,
        /// als er een email/username eerder is gemaakt dan komt er melding dat de username/email al bestaad.
        /// 

        ////Registration POST Action
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Registration(/*[Bind(Exclude = "IsEmailVerified, ActivationCode")]*/ User user)
        //{

        //    bool Status = false;
        //    string message;

        //    //Model Validation
        //    if (ModelState.IsValid)
        //    {
        //        #region //Email is already Exist
        //        var isExist = IsEmailExist(user.Email);
        //        if (isExist)
        //        {
        //            ModelState.AddModelError("EmailExist", "Email already exist");
        //        }
        //        #endregion

        //        #region //Generate Activation Code
        //        user.ActivationCode = Guid.NewGuid();
        //        #endregion

        //        #region //Password Hashing

        //        user.Password = Crypto.Hash(user.Password);
        //        user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
        //        #endregion

        //        user.IsEmailVerified = false;

        //        //#region //Save Data to Database

        //        //using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //        //{
        //        //    dc.Users.Add(User);
        //        //    dc.SaveChanges();

        //        //    //Send Email to User

        //        //}


        //        //#endregion



        //    }
        //    else
        //    {
        //        message = "Invalid Request";
        //    }

        //    //ViewBag.Message = message;
        //    ViewBag.Status = Status;
        //    return View(user);
        //}

        //private bool IsEmailExist(string email)
        //{
        //    throw new NotImplementedException();
        //}

        ////Verify Email

        ////Verify Email Link

        ////Login

        ////Login POST

        ////Logout
        ////[NonAction]
        ////public bool IsEmailExist(string Email)
        ////{
        ////    using (db.MyDatabaseEntities dc = new db.MyDatabaseEntities())
        ////    {
        ////        dc.Users.Add();
        ////        dc.SaveChanges();
        ////    }

        ////}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        ////Verify Account  

        ////[HttpGet]
        ////public ActionResult VerifyAccount(string id)
        ////{
        ////    bool Status = false;
        ////    using (MyDatabaseEntities dc = new MyDatabaseEntities())
        ////    {
        ////        dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
        ////        // Confirm password does not match issue on save changes
        ////        var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
        ////        if (v != null)
        ////        {
        ////            v.IsEmailVerified = true;
        ////            dc.SaveChanges();
        ////            Status = true;
        ////        }
        ////        else
        ////        {
        ////            ViewBag.Message = "Invalid Request";
        ////        }
        ////    }
        ////    ViewBag.Status = Status;
        ////    return View();
        ////}


    }
}