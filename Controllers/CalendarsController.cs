using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    public class CalendarsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IHabitsService _service;

        public CalendarsController(ApplicationDbContext context, IHabitsService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //creates a new calendar to display
            Calendars calendars = new Calendars(DateTime.Now);

            //retrieves all the habits and their dates
            var getAllHabits = await _service.GetHabitsByUserAndMonth(DateTime.Now, userId);


            //Not sure what this does, but something breaks if I remove it.
            //Solution! An if statement in Calendar/Index.cshtml used this line. It was needed because I did not use bring in the data from AllDates
            //var allDates = _context.AllDates.ToList();

            DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(calendars, getAllHabits);
            return View(viewModel);


        }

        public IActionResult ChangeMonth(string btnValue, DateTime currentMonth)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Calendars nextCalendar = new Calendars(currentMonth.AddMonths(+1));
            Calendars prevCalendar = new Calendars(currentMonth.AddMonths(-1));


            if (btnValue == "next")
            {
                List<HabitsDates> habitDates = _context.HabitsDates.Where(o => o.AllDates.Date.Month == nextCalendar.Month).Include(ad => ad.AllDates).Include(h => h.Habit).Where(hd => hd.Habit.UserId == userId).ToList();

                DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(nextCalendar, habitDates);
                return View("Index", viewModel);
            }
            else
            {
                List<HabitsDates> habitDates = _context.HabitsDates.Where(o => o.AllDates.Date.Month == prevCalendar.Month).Include(ad => ad.AllDates).Include(h => h.Habit).Where(hd => hd.Habit.UserId == userId).ToList();
                DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(prevCalendar, habitDates);
                return View("Index", viewModel);
            }
        }
    }
}
