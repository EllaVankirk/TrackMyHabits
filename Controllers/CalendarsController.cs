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
            //List of habitsdates
            List<HabitsDates> habitDates = context.HabitsDates.ToList();
            //List of AllDates
            List<AllDates> allTheDates = context.AllDates.ToList();

            //List of All the dates in the joined table. Just because it was in AllDates doesn't mean it has a habit associated with it.
            List<AllDates> selectedDates = (from d in context.AllDates
                                       join hd in context.HabitsDates
                                       on d.ID equals hd.AllDatesID
                                       where hd.AllDatesID == d.ID
                                       select d).ToList();

            //list of habits that are in the joined table. Just because it's in Habits doesn't mean it was had a date associated with it.
            var habits = (from h in context.Habits
                          join hd in context.HabitsDates
                          on h.ID equals hd.HabitsID
                          where hd.HabitsID == h.ID
                          select h).ToList();
            foreach (var date in calendars.RangeOfDates)
            {
                foreach(var day in allTheDates)
                {

                }

            }

            DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(calendars);
            return View(viewModel);
        }

        public IActionResult ChangeMonth(string btnValue, DateTime currentMonth)
        {
            Calendars nextCalendar = new Calendars(currentMonth.AddMonths(+1));
            Calendars prevCalendar = new Calendars(currentMonth.AddMonths(-1));

            var habits = (from h in context.Habits
                          join hd in context.HabitsDates
                          on h.ID equals hd.HabitsID
                          where hd.HabitsID == h.ID
                          select h).ToList();

            if (btnValue == "next")
            {
                DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(nextCalendar);
                return View("Index", viewModel);
            }
            else
            {
                DisplayHabitsOnCalendarViewModel viewModel = new DisplayHabitsOnCalendarViewModel(prevCalendar);
                return View("Index", viewModel);
            }
        }
    }
}
