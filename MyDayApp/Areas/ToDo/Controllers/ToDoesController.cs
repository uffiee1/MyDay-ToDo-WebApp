using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDayApp.Models;
using Microsoft.AspNetCore.Identity;
using MyDayApp.BusinessLogic.ToDoLogic.Interfaces;
using MyDayApp.DataAccess;

namespace MyDayApp.Controllers
{
    [Area("ToDo")]
    public class ToDoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IToDoLogic _toDoLogic;

        public ToDoesController(AppDbContext context, IToDoLogic toDoLogic)
        {
            _context = context;
            _toDoLogic = toDoLogic;
        }

        // GET: ToDoes
        public async Task<IActionResult> Index(ToDo model)
        {

            //Lamda Expression
            IEnumerable<ToDo> result; 
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            model.UserId = claim.Value;

            if (User.IsInRole(Role.Administrator))
            {
                result = await _toDoLogic.GetAllToDoList(model);
            }
            else
            {
                result = await _toDoLogic.GetAllToDoListAsUser(model);
            }

            return View(result);
        }

        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .FirstOrDefaultAsync(m => m.ToDoID == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            var todo = new ToDo();
            return View(todo);
        }

        // POST: ToDoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                toDo.User = _context.User.FirstOrDefault(a => a.Id == claim.Value);

                toDo.UserId = claim.Value;

                await _toDoLogic.CreateToDo(toDo);

                _context.Add(toDo);
                return RedirectToAction(nameof(Index));
            }
            return View(toDo);
        }

        //[Authorize(Roles = Role.Gebruiker)]
        // GET: ToDoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToDoID,Event,Location,Status,UserId")] ToDo toDo)
        {
            if (id != toDo.ToDoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                toDo.User = _context.User.FirstOrDefault(a => a.Id == claim.Value);
                
                try
                {
                    await _toDoLogic.EditToDo(toDo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.ToDoID))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDo);
        }

        // GET: ToDoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .FirstOrDefaultAsync(m => m.ToDoID == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDo = await _context.ToDo.FindAsync(id);
            
            await _toDoLogic.DeleteToDo(toDo);

            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.ToDoID == id);
        }
    }
}
