using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            AddHabitsDatesViewModel viewModel = new AddHabitsDatesViewModel(theHabit);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddHabit(AddHabitsDatesViewModel viewModel)
        {
            //if (ModelState.IsValid)
            // {
            //     int habitID = viewModel.Habits.Id;
            //     var date = viewModel.AllDates.Date;

            //     //All of the dates.
            //     var existingDates = _context.AllDates;

            //     //Loop through the dates, and if there is no match, add it to the DB.
            //     foreach (var day in existingDates)
            //     {
            //         if (viewModel.AllDates.Date.ToLongDateString() != day.Date.ToLongDateString())
            //         {
            //             _context.AllDates.Add(viewModel.AllDates);
            //         }

            //          //then what?
            //     }
            //     return Redirect("/Habits/Details/" + habitID);
            // }

            if (ModelState.IsValid)
            {
                //sets a variable to be the ID of each thing.
                int habitID = viewModel.Habits.Id;
                int dateID = viewModel.AllDates.Id;

                //uses the variables above to create a list of items that are already in the database using the current habitID and dateID
                List<HabitsDates> existingItems = _context.HabitsDates
                    .Where(hd => hd.HabitsId == habitID)
                    .Where(hd => hd.AllDatesId == dateID)
                    .ToList();

                //if there isn't anything in the list of existingItems, then add it. If there is something, nothing happens.
                if (existingItems.Count == 0)
                {
                    HabitsDates habitsDates = new HabitsDates
                    {
                        HabitsId = habitID,
                        AllDatesId = dateID
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
            if (id == null)
            {
                return NotFound();
            }
            var dates = await _context.AllDates
                .FirstOrDefaultAsync(d => d.Id == id);
            if (dates == null)
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
            return _context.AllDates.Any(e => e.Id == id);
        }
    }
}