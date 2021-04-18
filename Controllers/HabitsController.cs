using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrackMyHabit.Data;
using TrackMyHabit.Models;
using TrackMyHabit.Models.HabitsViewModels;

namespace TrackMyHabit.Controllers
{
    [Authorize]
    public class HabitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HabitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Habits
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Habits.ToListAsync());
        //}

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var habits = from h in _context.Habits
                         select h;


            if (!String.IsNullOrEmpty(searchString))
            {
                habits = habits.Where(h => h.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    habits = habits.OrderByDescending(h => h.Name);
                    break;
                case "Date":
                    habits = habits.OrderBy(h => h.Date);
                    break;
                case "date_desc":
                    habits = habits.OrderByDescending(h => h.Date);
                    break;
                default:
                    habits = habits.OrderBy(h => h.Name);
                    break;
            }
            return View(await habits.AsNoTracking().ToListAsync());
        }

        // GET: Habits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<HabitsDates> habitsDates = _context.HabitsDates
                .Where(hd => hd.HabitsID == id)
                .Include(hd => hd.AllDates)
                .ToList();

            var habits = await _context.Habits
                .FirstOrDefaultAsync(m => m.ID == id);
            if (habits == null)
            {
                return NotFound();
            }

            HabitDetailsViewModel viewModel = new HabitDetailsViewModel(habits, habitsDates);
            return View(viewModel);
        }

        // GET: Habits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,HabitInitial,Colour")] Habits habits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habits);
        }

        // GET: Habits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habits = await _context.Habits.FindAsync(id);
            if (habits == null)
            {
                return NotFound();
            }
            return View(habits);
        }

        // POST: Habits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date")] Habits habits)
        {
            if (id != habits.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitsExists(habits.ID))
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
            return View(habits);
        }

        // GET: Habits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habits = await _context.Habits
                .FirstOrDefaultAsync(m => m.ID == id);
            if (habits == null)
            {
                return NotFound();
            }

            return View(habits);
        }

        // POST: Habits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habits = await _context.Habits.FindAsync(id);
            _context.Habits.Remove(habits);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitsExists(int id)
        {
            return _context.Habits.Any(e => e.ID == id);
        }
    }
}
