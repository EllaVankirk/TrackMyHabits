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
            Calendars calendars = new Calendars();
            return View(calendars);
        }

        public IActionResult ChangeMonth(string btnValue)
        {
            Calendars calendar = new Calendars();

            if (btnValue == "next")
            {
                calendar.Day = new DateTime(2020, calendar.Month, 1);
            }
            return View("Index", calendar);
        }
    }
}
