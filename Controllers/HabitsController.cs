using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Data;
using TrackMyHabit.Data.Services;
using TrackMyHabit.Models;
using TrackMyHabit.Models.HabitsViewModels;

namespace TrackMyHabit.Controllers
{
    //[Authorize]
    public class HabitsController : Controller
    {
        private readonly IHabitsService _service;

        public HabitsController(IHabitsService service)
        {
            _service = service;
        }

        //GET: Habits
        //Index Sort and Search method
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var habits = await _service.GetAllAsync();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                habits = habits.Where(h => h.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    habits = habits.OrderByDescending(h => h.Name);
                    break;
                default:
                    habits = habits.OrderBy(h => h.Name);
                    break;
            }

            return View(habits);
        }

        // GET: Habits/Details/5
        //TODO: Implement
        public async Task<IActionResult> Details(int id)
        {
            var habitsDetails = await _service.GetHabitByIdAsync(id);
            if (habitsDetails == null)
            {
                return NotFound();
            }

            return View(habitsDetails);
        }

        // GET: Habits/Create
        public IActionResult Create()
        {
            return View();
        }


        //FIXME
        // POST: Habits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHabitWithDateViewModel habits)
        {
            await _service.CreateHabitAsync(habits);
            return View(habits);
        }

        // GET: Habits/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //I need to make this a viewmodel
            var habitsDetails = await _service.GetHabitByIdAsync(id);
            var habits = new UpdateHabitWithDateViewModel()
            {
                HabitId = habitsDetails.Id,
                HabitName = habitsDetails.Name,
                HabitColor = habitsDetails.Colour,
                HabitsDates = habitsDetails.HabitsDates
            };

            if (habits == null)
            {
                return NotFound();
            }
            return View(habits);
        }

        // POST: Habits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateHabitWithDateViewModel habit)
        {
            var listOfHabits = await _service.GetAllAsync();
            if (!ModelState.IsValid)
            {
                return View(habit);
            }
            await _service.AddDateToHabit(habit);
            return View("Index", listOfHabits);
        }

        // GET: Habits/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var habitDetails = await _service.GetHabitByIdAsync(id);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return View(habitDetails);
        }

        // POST: Habits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitDetails = await _service.GetHabitByIdAsync(id);
            if (habitDetails == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //I don't need a view I don't think, I just need to be able to us this method ?
        public async void CleanUpDates()
        {
            await _service.DeleteEmptyDates();
        }

        //private bool HabitsExists(int id)
        //{
        //    return _context.Habits.Any(e => e.Id == id);
        //}
    }
}
