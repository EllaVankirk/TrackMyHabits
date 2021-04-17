using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrackMyHabit.Data;
using TrackMyHabit.Models;

namespace TrackMyHabit.Controllers
{
    public class AllDatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllDatesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllDates.ToListAsync());
        }

        public IActionResult Add()
        {
            AllDates dates = new AllDates();
            return View(dates);
        }

        [HttpPost]
        public IActionResult Add(AllDates dates)
        {
            if (ModelState.IsValid)
            {
                _context.AllDates.Add(dates);
                _context.SaveChanges();
                return Redirect("/AllDates");
            }
            return View("Add", dates);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var dates = await _context.AllDates
                .FirstOrDefaultAsync(d => d.ID == id);
            if ( dates == null)
            {
                return NotFound();
            }

            return View(dates);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dates = await _context.AllDates.FindAsync(id);
            _context.AllDates.Remove(dates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatesExists(int id)
        {
            return _context.AllDates.Any(e => e.ID == id);
        }
    }
}
