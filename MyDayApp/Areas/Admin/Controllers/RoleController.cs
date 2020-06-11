﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyDayApp.Areas.Admin.Models;
using MyDayApp.DataAccess;
using MyDayApp.Models;

namespace MyDayApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = Role.Administrator)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public IActionResult Index()
        {
            IQueryable<IdentityRole> roles = roleManager.Roles;
            return View(roles);
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Role");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // Role ID is passed from the URL to the action
        [HttpGet]
        public async Task<IActionResult> RoleEdit(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role met Id = {id} kan niet worden gevonden";
                return View("NotFound");
            }

            var model = new RoleEditModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.UserList.Add(user.UserName);
                }
            }
            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role met Id = {model.RoleId} kan niet worden gevonden";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol met Id = {roleId} kan niet worden gevonden";
                return View("NotFound");
            }

            var model = new List<RoleModel>();

            foreach (var user in userManager.Users)
            {
                var roleModel = new RoleModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    roleModel.IsSelected = true;
                }
                else
                {
                    roleModel.IsSelected = false;
                }

                model.Add(roleModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<RoleModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol met Id = {roleId} kan niet worden gevonden";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;
                
                //Also check if the user is already a member of te given role name
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                //If te user is not selected and is in the Role; Remove
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("RoleEdit", new { Id = roleId });
                }
            }
            return RedirectToAction("RoleEdit", new { Id = roleId });
        }


        ////[Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        //public ActionResult Delete(string id = null)
        //{
        //    var Db = new AppDbContext();
        //    var user = Db.Users.First(u => u.UserName == id);
        //    var model = new EditUserViewModel(user);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(model);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    var Db = new ApplicationDbContext();
        //    var user = Db.Users.First(u => u.UserName == id);
        //    Db.Users.Remove(user);
        //    Db.SaveChanges();
        //    return RedirectToAction("Index");
        //}




        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }
        }
    }
}
