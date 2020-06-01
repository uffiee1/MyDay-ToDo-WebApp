using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDayApp.DataAccess;
using MyDayApp.DataAccess.Data.Repository.IRepository;
using MyDayApp.Models;

namespace MyDayApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Administrator)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;
        private readonly AppDbContext _context;


        public UserController(IUnitOfWork unitOfWork, AppDbContext db, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //var userList = _unitOfWork.User.GetAll().ToList();

            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            //foreach (var user in userList)
            //{
            //    var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id)?.RoleId;
            //    user.Role = roles.FirstOrDefault(u => u.Id == roleId)?.Name;
            //}
            
            //return View(userList);

            return View(await _context.User.ToListAsync());
        }

        //public IActionResult Lock(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.User.LockedUser(id);
        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult Unlock(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.User.UnlockUser(id);
        //    return RedirectToAction(nameof(Index));
        //}


    }
}
