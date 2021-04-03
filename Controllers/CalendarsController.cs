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
        public IActionResult Index(string? name)
        {
            Calendars calendars = new Calendars();
            return View(calendars);
        }

        [HttpPost]
        public IActionResult ChangeMonth (string name, Calendars calendar)
        {
            Calendars calendars = new Calendars();
            if (name == "previous")
            {
                calendars.Month = calendars.Month - 1;
            }
            else if (name == "next")
            {
                calendars.Month = calendars.Month + 1;
            }

            return View(calendars);
        }
    }
}
