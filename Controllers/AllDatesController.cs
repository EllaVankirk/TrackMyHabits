using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //    //TODO: Check if a date exists in database
            //    //If it exists, assign it to the habit in question
            //    //If it doesnt, add it to database then assign it to habit
            //    int habitId = viewModel.HabitID;

            //    var dates = _context.AllDates;
            //    foreach (var day in dates)
            //    {
            //        if (day.Date.ToLongDateString() != viewModel.Date.ToLongDateString())
            //        {
            //            _context.AllDates.Date.Add(dates);
            //            _context.SaveChanges();
            //        }
            //        else
            //        {
            //            //create it
            //        }
            //    }

            //    //if (ModelState.IsValid)
            //    //{
            //    //    //sets a variable to be the ID of each thing.
            //    //    int habitID = viewModel.HabitID;
            //    //    int dateID = viewModel.AllDatesID;

            //    //    //uses the variables above to create a list of items that are already in the database using the current habitID and dateID
            //    //    List<HabitsDates> existingItems = _context.HabitsDates
            //    //        .Where(hd => hd.HabitsID == habitID)
            //    //        .Where(hd => hd.AllDatesID == dateID)
            //    //        .ToList();

            //    //    //if there isn't anything in the list of existingItems, then add it. If there is something, nothing happens.
            //    //    if (existingItems.Count == 0)
            //    //    {
            //    //        HabitsDates habitsDates = new HabitsDates
            //    //        {
            //    //            HabitsID = habitID,
            //    //            AllDatesID = dateID
            //    //        };

            //    //        _context.HabitsDates.Add(habitsDates);
            //    //        _context.SaveChanges();
            //    //    }
            //    //    return Redirect("/Habits/Details/" + habitID);
            //    //}
            return View(viewModel);
    }

    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dates = await _context.AllDates
                .FirstOrDefaultAsync(d => d.ID == id);
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
            return _context.AllDates.Any(e => e.ID == id);
        }
    }
}