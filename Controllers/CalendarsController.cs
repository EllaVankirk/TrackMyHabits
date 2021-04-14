using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Data;
using TrackMyHabit.Models;

namespace TrackMyHabit.Controllers
{

    public class CalendarsController : Controller
    {
        private ApplicationDbContext context;

        public CalendarsController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            var habit = context.Habits.Include(h => h.HabitInitial);
            Calendars calendars = new Calendars(DateTime.Now);
            calendars.Habit = habit.ToList();


            return View(calendars);
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
