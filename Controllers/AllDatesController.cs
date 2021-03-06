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
    //[Authorize]
    public class AllDatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllDatesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allDates = from ad in _context.AllDates
                           select ad;
            return View(await allDates.AsNoTracking().ToListAsync());
        }

        public IActionResult Add()
        {
            return View();
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
            else
            {
              ViewBag.ErrorMessage = "This date has already been added.";
            }
            return View("Add", dates);
        }


        // responds to URLs like /Tag/AddEvent/5 (where 5 is an event ID)
        public IActionResult AddHabit(int id)
        {
            Habits theHabit = _context.Habits.Find(id);
            List<AllDates> possibleDates = _context.AllDates.ToList();

            AddHabitsDatesViewModel viewModel = new AddHabitsDatesViewModel(theHabit, possibleDates);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddHabit(AddHabitsDatesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int habitID = viewModel.HabitID;
                int dateID = viewModel.AllDatesID;

                List<HabitsDates> existingItems = _context.HabitsDates
                    .Where(hd => hd.HabitsID == habitID)
                    .Where(hd => hd.AllDatesID == dateID)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    HabitsDates habitsDates = new HabitsDates
                    {
                        HabitsID = habitID,
                        AllDatesID = dateID
                    };

                    _context.HabitsDates.Add(habitsDates);
                    _context.SaveChanges();
                }
                return Redirect("/Habits/Details/" + habitID);
            }
            return View(viewModel);
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
