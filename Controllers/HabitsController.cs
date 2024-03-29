﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrackMyHabit.Data;
using TrackMyHabit.Data.Services;
using TrackMyHabit.Models;
using TrackMyHabit.Models.HabitsViewModels;

namespace TrackMyHabit.Controllers
{
    [Authorize]
    public class HabitsController : Controller
    {
        private readonly IHabitsService _service;


        public HabitsController(IHabitsService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var habits = await _service.GetAllHabitsByUserAsync(userId);
            return View(habits);
        }

        //GET: Habits
        //Index Sort and Search method
        //public async Task<IActionResult> Index(string sortOrder, string searchString)
        //{
        //    var habits = await _service.GetAllAsync();

        //    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        //    ViewData["CurrentFilter"] = searchString;


        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        habits = habits.Where(h => h.Name.Contains(searchString));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            habits = habits.OrderByDescending(h => h.Name);
        //            break;
        //        default:
        //            habits = habits.OrderBy(h => h.Name);
        //            break;
        //    }

        //    return View(habits);
        //}

        // GET: Habits/Details/5

        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var habitsDetails = await _service.GetHabitByIdAsync(id, userId);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.CreateHabitAsync(habits, userId);
            return RedirectToAction(nameof(Index));
        }


        // GET: Habits/Edit/5
        [HttpGet]
        public async Task<IActionResult> AddDate(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var habitsDetails = await _service.GetHabitByIdAsync(id, userId);
            var habits = new AddHabitToDateViewModel()
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
        public async Task<IActionResult> AddDateAsync(AddHabitToDateViewModel habit)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                return View(habit);
            }
            await _service.AddNewDateToHabitAsync(habit);
            var listOfHabits = await _service.GetAllHabitsByUserAsync(userId);
            return View("Index", listOfHabits);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var habit = await _service.GetHabitByIdAsync(id, userId);
            return View(habit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Colour")] Habits habit)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.UpdateAsync(id, habit);
            var updatedHabit = await _service.GetHabitByIdAsync(id, userId);
;            return View(updatedHabit);
        }

        // GET: Habits/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var habitDetails = await _service.GetHabitByIdAsync(id, userId);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var habitDetails = await _service.GetHabitByIdAsync(id, userId);
            if (habitDetails == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //I don't need a view I don't think, I just need to be able to us this method ?
        //public async void CleanUpDates()
        //{
        //    await _service.DeleteEmptyDates();
        //}

        //private bool HabitsExists(int id)
        //{
        //    return _context.Habits.Any(e => e.Id == id);
        //}
    }
}
