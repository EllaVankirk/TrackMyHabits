using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Data;
using TrackMyHabit.Models;
using TrackMyHabit.Models.HabitsViewModels;

namespace TrackMyHabit.Controllers
{
    [Authorize]
    public class CalendarsController : Controller
    {
        private ApplicationDbContext context;

        public CalendarsController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {

            Calendars calendars = new Calendars(DateTime.Now);

            List<HabitsDates> habitDates = context.HabitsDates.Where(o => o.AllDates.Date.Month == calendars.Month).Include(h => h.Habit).ToList();

            DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(calendars, habitDates);
            return View(viewModel);
        }

        public IActionResult ChangeMonth(string btnValue, DateTime currentMonth)
        {
            Calendars nextCalendar = new Calendars(currentMonth.AddMonths(+1));
            Calendars prevCalendar = new Calendars(currentMonth.AddMonths(-1));

            if (btnValue == "next")
            {
                List<HabitsDates> habitDates = context.HabitsDates.Where(o => o.AllDates.Date.Month == nextCalendar.Month).Include(h => h.Habit).Include(ad => ad.AllDates).ToList();
                DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(nextCalendar, habitDates);
                return View("Index", viewModel);
            }
            else
            {
                List<HabitsDates> habitDates = context.HabitsDates.Where(o => o.AllDates.Date.Month == prevCalendar.Month).Include(h => h.Habit).Include(ad => ad.AllDates).ToList();
                DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(prevCalendar, habitDates);
                return View("Index", viewModel);
            }
        }
    }
}
