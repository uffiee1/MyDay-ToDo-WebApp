using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDayApp.Models;
using Microsoft.AspNetCore.Identity;
using MyDayApp.DataAccess;
using MySql.Data.MySqlClient;

namespace MyDayApp.Controllers
{
    [Area("ToDo")]
    public class ToDoesController : Controller
    {
        MySqlConnection conn = new MySqlConnection("Server = localhost; User Id = root; Password = ''; Database = ufukdb");
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        private readonly AppDbContext _context;
        string username = "";
        string currentUserId = "0";

        public ToDoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ToDoes
        public async Task<IActionResult> Index()
        {
            //string query = $"SELECT * FROM `users` WHERE `UsernameID` = '{username}'";
            //MySqlCommand comm = new MySqlCommand(query, conn);
            //conn.Open();
            //MySqlDataReader reader = comm.ExecuteReader();
            //while (reader.Read())
            //{
            //    Gebeurtenis g = new Gebeurtenis();
            //    g.ID = reader.GetValue(0).ToString();
            //    g.Event = reader.GetValue(1).ToString();
            //    g.Date = reader.GetValue(2).ToString();
            //    g.EndDate = reader.GetValue(3).ToString();
            //    g.Location = reader.GetValue(4).ToString();
            //    g.UsernameId = reader.GetValue(5).ToString();
            //    g.Status = reader.GetValue(6).ToString();
            //}
            //reader.Close();
            //conn.Close();

            return View(await _context.ToDo.ToListAsync());
        }

        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Event,Location,Status")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {

                 
                _context.Add(toDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDo);
        }

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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Event,Location,Status")] ToDo toDo)
        {
            if (id != toDo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
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
                .FirstOrDefaultAsync(m => m.ID == id);
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
            _context.ToDo.Remove(toDo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.ID == id);
        }
    }
}
