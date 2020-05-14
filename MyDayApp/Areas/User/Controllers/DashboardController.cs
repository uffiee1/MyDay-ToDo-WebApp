using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyDayApp.Controllers
{
    public class DashboardController : Controller
    {
        [Area("User")]

        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Profile()
        {
        //    if (HttpContext.Session["UID"] != null)
        //    {
        //        ViewBag.Username = HttpContext.Session["username"];
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Account");
        //    }
        return View();
        }

    }
}