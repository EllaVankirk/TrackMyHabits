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

            //List of Habit Dates populated with the dates
            List<HabitsDates> habitDates = (from hd in context.HabitsDates
                                            join ad in context.AllDates
                                            on hd.AllDatesID equals ad.ID
                                            where ad.ID == hd.AllDatesID
                                            select hd).ToList();

            var habits = (from h in context.Habits
                          join hd in context.HabitsDates
                          on h.ID equals hd.HabitsID
                          where hd.HabitsID == h.ID
                          select h).ToList();
            DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(habits, habitDates, calendars);
            return View(viewModel);
        }

        public IActionResult ChangeMonth(string btnValue, DateTime currentMonth)
        {
            Calendars nextCalendar = new Calendars(currentMonth.AddMonths(+1));
            Calendars prevCalendar = new Calendars(currentMonth.AddMonths(-1));
            if (btnValue == "next")
            {
                return View("Index", nextCalendar);
            }
            else
            {
                return View("Index", prevCalendar);
            }
        }
    }
}
