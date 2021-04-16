using Microsoft.AspNetCore.Authorization;
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
            List<Habits> habit = context.Habits.ToList();


            return View(calendars);
        }

        public IActionResult Index2()
        {

            return View();
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
