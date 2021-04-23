using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyHabit.Models.HabitsViewModels
{
    public class DisplayHabitsOnCalendarViewModel
    {
        public List<HabitsDates> HabitDates { get; set; }

        public DateTime Day { get; set; } = DateTime.Now;
        public DateTime DisplayDate { get; set; }
        public DateTime FirstOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public IEnumerable<DateTime> CalendarDates { get; set; }

        public DisplayHabitsOnCalendarViewModel(Calendars theCalendar, List<HabitsDates> habitsDates)
        {
            DisplayDate = theCalendar.DisplayDate;
            Month = DisplayDate.Month;
            Year = DisplayDate.Year;
            FirstOfMonth = new DateTime(DisplayDate.Year, DisplayDate.Month, 1);
            StartDate = FirstOfMonth.AddDays(-(int)FirstOfMonth.DayOfWeek);
            CalendarDates = Enumerable.Range(0, 42).Select(i => StartDate.AddDays(i));

            HabitDates = habitsDates;
        }
    }
}
