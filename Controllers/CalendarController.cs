using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyHabit.Models;

namespace TrackMyHabit.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            //retrieve the days ?
            CalendarApp firstCalendar = new CalendarApp()
            {
                HabitDay = DateTime.Now
            };
            return View(firstCalendar);
        }
    }
}
